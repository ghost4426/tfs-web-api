using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
    public class UserRepositoryImpl : GenericRepository<User> , IUserRepository
    {

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

        public Task<User> FindByUsername(string username)
        {
            return FindAsync(u => u.Username.CompareTo(username) == 0);
        }
    }
}
