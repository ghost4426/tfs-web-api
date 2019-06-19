using BusinessLogic.IBusinessLogic;
using DTO.Entities;
using DTO.Models.Exception;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using DataAccess.IRepositories;
using System.Linq;
using Models = DTO.Models;


namespace BusinessLogic.BusinessLogicImpl
{
    public class UserBLImpl : IUserBL
    {

        private IUserRepository _userRepos;

        public UserBLImpl(IUserRepository userRepository)
        {
            if (userRepository != null)
                this._userRepos = userRepository;
        }

        

        public async Task<int> CreateUser(User newUser)
        {
            var hashedPassword = PasswordHasher.GetHashPassword(newUser.Password);
            newUser.Password = hashedPassword.HashedPassword;
            newUser.Salt = hashedPassword.Salt;
            return await this._userRepos.CreateUser(newUser);
        }
        public async Task<IList<User>> GetUsers()
        {
            
            IList<User> users = await _userRepos.FindAllAsync(u => u.RoleId > 1);

            return users;

        }

        public async Task<User> CheckLogin(Models.LoginRequest loginInfo)
        {
            var user = await this._userRepos.FindByUsername(loginInfo.Username);
            if (user != null)
            {
                var isCorrectPassword = PasswordHasher.CheckHashedPassword(new Models.HashPassword()
                {
                    HashedPassword = user.Password,
                    Password = loginInfo.Password,
                    Salt = user.Salt
                });
                if (isCorrectPassword)
                {
                    return user;
                }
            }
            throw new InvalidUsernameOrPasswordException("Wrong Username or Password");
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepos.GetByIdAsync(id);
        }
    }
}
