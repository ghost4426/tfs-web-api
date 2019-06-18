using BusinessLogic.IBusinessLogic;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using DataAccess.IRepositories;
using System.Linq;


namespace BusinessLogic.BusinessLogicImpl
{
   public class UserBLImpl : IUserBL
    {

        private IUserRepository repos;

        public UserBLImpl(IUserRepository userRepository)
        {
            if (userRepository != null)
                this.repos = userRepository;
        }

        public async Task<int> CreateUser(User newUser)
        {
          var hashedPassword =  PasswordHasher.GetHashPassword(newUser.Password);
            newUser.Password = hashedPassword.HashedPassword;
            newUser.Salt = hashedPassword.Salt;
            return await this.repos.CreateUserAsync(newUser);
        }
        public async Task<IList<User>> GetUsers()
        {
            
            IList<User> users = await this.repos.FindAllAsync(u => u.RoleId > 1);

            return users;

        }
    }
}
