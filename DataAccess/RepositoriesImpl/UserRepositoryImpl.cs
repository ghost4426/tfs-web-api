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

        public UserRepositoryImpl(FoodTrackerDbContext context)
           : base(context)
        {
            foodTrackerDbContext = context;
        }
        public async Task<int> CreateUserAsync(User newUser)
        {
            newUser.UserId = 0;
            newUser.CreatedDate = DateTime.Now;
            await this.InsertAsync(newUser, true);
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
        }
    }
}
