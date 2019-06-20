using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace DataAccess.RepositoriesImpl
{
    public class UserRepositoryImpl : GenericRepository<User> , IUserRepository
    {
        private IUserRepository repos;
        private FoodTrackerDbContext foodTrackerDbContext;

        private FoodTrackingDbContext foodTrackingDbContext;


        public UserRepositoryImpl(FoodTrackingDbContext context)
           : base(context)
        {
            foodTrackingDbContext = context;
        }
        public async Task<int> CreateUser(User newUser)
        {
            newUser.UserId = 0;
            newUser.CreatedDate = DateTime.Now;
            await InsertAsync(newUser, true);
            //this.Commit();
            return newUser.UserId;
        }

        public async Task<string> changeRole1User(User user)
        {
            await this.UpdateAsync(user, true);
            return user.RoleId.ToString();
        }

        public Task<IList<User>> GetUsers()
        {
            return repos.GetUsers();


        public Task<User> FindByUsername(string username)
        {
            return FindAsync(u => u.Username.CompareTo(username) == 0);
        }
        public async Task<IList<User>> GetUsers()
        {
            IList<User> users = await FindAllAsync(u => u.RoleId > 1);
            return users;
        }
    }
}
