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
using Common.Constant;


namespace BusinessLogic.BusinessLogicImpl
{
    public class UserBLImpl : IUserBL
    {

        private IUserRepository _userRepos;
        private IRoleRepository _roleRepos;

        public UserBLImpl(IUserRepository userRepos, IRoleRepository roleRepos)
        {
            _userRepos = userRepos;
            _roleRepos = roleRepos;
        }


        public async Task<bool> CreateUser(User newUser)
        {
            var hashedPassword = PasswordHasher.GetHashPassword(newUser.Password);
            var user = await _userRepos.FindByUsername(newUser.Username);
            if (user != null)
            {
                throw new DulicatedUsernameException(msg: MessageConstant.DUPLICATED_USERNAME);
            }
            newUser.UserId = 0;
            newUser.Password = hashedPassword.HashedPassword;
            newUser.Salt = hashedPassword.Salt;
            newUser.CreatedDate = DateTime.Now;
            newUser.IsActive = true;
            _userRepos.Insert(newUser, true);
            if (newUser.UserId > 0)
            {
                return true;
            }
            return false;
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
                    user.Role =  _roleRepos.GetById(user.RoleId);
                    return user;
                }
            }
            throw new InvalidUsernameOrPasswordException(msg: MessageConstant.WRONG_PASS_OR_USERNAME);
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepos.GetByIdAsync(id);
        }

        public async Task RemoveByIdAsync(int id)
        {
           await _userRepos.DeleteAsync(id, true);
        }
    }
}
