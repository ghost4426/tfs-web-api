using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Threading.Tasks;


namespace DataAccess.RepositoriesImpl
{
    public class UserRepositoryImpl : GenericRepository<User>, IUserRepository
    {
        private FoodTrackingDbContext _dbContext;


        public UserRepositoryImpl(FoodTrackingDbContext dbContext)
           : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> changeRole1User(User user)
        {
            await this.UpdateAsync(user, true);
            return user.RoleId.ToString();
        }
        public Task<User> FindByUsername(string username)
        {
            return FindAsync(u => u.Username == username);

        }
        public async Task<User> UpdateUser(User user)
        {
            await this.UpdateAsync(user, true);
            return user;
        }

    }
}
