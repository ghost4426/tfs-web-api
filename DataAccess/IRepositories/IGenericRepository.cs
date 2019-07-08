using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
        IList<TEntity> GetAllMatched(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(object id);
        TEntity Find(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> GetIQueryable();
        IList<TEntity> GetAllPaged(int pageIndex, int pageSize, out int totalCount);
        int Count();
        object Insert(TEntity entity, bool saveChanges = true);
        void Delete(object id, bool saveChanges = true);
        void Delete(TEntity entity, bool saveChanges = true);
        void Update(TEntity entity, bool saveChanges = true);
        TEntity Update(TEntity entity, object key, bool saveChanges = true);
        void Commit();

        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetTopAsync(Expression<Func<TEntity, bool>> orderBy, int top);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<IList<TEntity>> FindTopAsync<TSortedBy>(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, TSortedBy>> orderBy, int top);
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        Task<int> CountAsync();
        Task<object> InsertAsync(TEntity entity, bool saveChanges = true);
        Task DeleteAsync(object id, bool saveChanges = true);
        Task DeleteAsync(TEntity entity, bool saveChanges = true);
        Task UpdateAsync(TEntity entity, bool saveChanges = true);
        Task UpdateRangeAsync(ICollection<TEntity> entities, bool saveChanges = true);
        Task<TEntity> UpdateAsync(TEntity entity, object key, bool saveChanges = true);
        Task CommitAsync();
        void Dispose();

    }
}
