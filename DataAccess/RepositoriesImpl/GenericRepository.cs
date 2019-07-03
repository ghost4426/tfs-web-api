using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        // Instance of the DbContext. Must be passed or injected.        
        private DbContext Context { get; set; }
        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        //Internally re-usable DbSet instance.
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<TEntity>();
                return _dbSet;
            }
        }
        private DbSet<TEntity> _dbSet;

        #region Regular Members
        public virtual IList<TEntity> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IList<TEntity> GetAllMatched(Expression<Func<TEntity, bool>> match)
        { 
            return this.DbSet.Where(match).ToList();
        }

        public IList<TEntity> GetAllMatchedOrderBy(Expression<Func<TEntity, bool>> match)
        {
            return this.DbSet.Where(match).ToList();
        }
        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = this.DbSet;
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }
            return queryable;
        }

        public virtual TEntity GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return this.DbSet.SingleOrDefault(match);
        }

        public virtual IQueryable<TEntity> GetIQueryable()
        {
            return this.DbSet.AsQueryable<TEntity>();
        }

        public virtual IList<TEntity> GetAllPaged(int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = this.DbSet.Count();
            return this.DbSet.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        public int Count()
        {
            return this.DbSet.Count();
        }

        public virtual object Insert(TEntity entity, bool saveChanges = true)
        {
            var rtn = this.DbSet.Add(entity);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
            return rtn;
        }

        public virtual void Delete(object id, bool saveChanges = true)
        {
            var item = GetById(id);
            this.DbSet.Remove(item);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity, bool saveChanges = true)
        {
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Update(TEntity entity, bool saveChanges = true)
        {
            var entry = Context.Entry(entity);
            this.DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual TEntity Update(TEntity entity, object key, bool saveChanges = true)
        {
            if (entity == null)
                return null;
            var exist = this.DbSet.Find(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(entity);
                if (saveChanges)
                {
                    Context.SaveChanges();
                }
            }
            return exist;
        }

        public virtual void Commit()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Async Members
        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.DbSet.Where(match).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.DbSet.SingleOrDefaultAsync(match);
        }

        public async Task<int> CountAsync()
        {
            return await this.DbSet.CountAsync();
        }

        public virtual async Task<object> InsertAsync(TEntity entity, bool saveChanges = true)
        {
            var rtn = await this.DbSet.AddAsync(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
            return rtn;
        }

        public virtual async Task DeleteAsync(object id, bool saveChanges = true)
        {
            this.DbSet.Remove(GetById(id));
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, bool saveChanges = true)
        {
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            var entry = Context.Entry(entity);
            this.DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, object key, bool saveChanges = true)
        {
            if (entity == null)
                return null;
            var exist = await this.DbSet.FindAsync(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(entity);
                if (saveChanges)
                {
                    await Context.SaveChangesAsync();
                }
            }
            return exist;
        }

        public virtual async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
        #endregion

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IList<TEntity>> FindTopAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, bool>> orderBy, int top)
        {
            return await this.DbSet.Where(match).OrderBy(orderBy).Take(top).ToListAsync();
        }

        public async Task<IList<TEntity>> GetTopAsync(Expression<Func<TEntity, bool>> orderBy, int top)
        {
            return await this.DbSet.Take(top).OrderBy(orderBy).ToListAsync();
        }

        public async Task UpdateRangeAsync(ICollection<TEntity> entities, bool saveChanges = true)
        {
            var entry = Context.Entry(entities);
            this.DbSet.AttachRange(entities);
            entry.State = EntityState.Modified;
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }
    }
}
