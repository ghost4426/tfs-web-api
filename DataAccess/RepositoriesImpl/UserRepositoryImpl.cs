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

        private FoodTrackingDbContext foodTrackingDbContext;

        public UserRepositoryImpl(FoodTrackingDbContext context)
           : base(context)
        {
            foodTrackingDbContext = context;
        }

        public Task<User> FindByUsername(string username)
        {
            return FindAsync(u => u.Username == username);
        }

        public async Task<IList<User>> GetUsers()
        {
            IList<User> users = await FindAllAsync(u => u.RoleId > 1);
            return users;
        }
    }
}
