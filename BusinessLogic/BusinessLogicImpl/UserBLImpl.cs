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
using Entities = DTO.Entities;


namespace BusinessLogic.BusinessLogicImpl
{
    public class UserBLImpl : IUserBL
    {
        private IUserRepository _userRepos;
        private IRoleRepository _roleRepos;
        public UserBLImpl(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            if (userRepository != null)
                this._userRepos = userRepository;
            if (roleRepository != null)
                this._roleRepos = roleRepository;
        }
        public async Task<int> CreateUser(User newUser)
        {
            var hashedPassword = PasswordHasher.GetHashPassword(newUser.Password);
            newUser.Password = hashedPassword.HashedPassword;
            newUser.Salt = hashedPassword.Salt;
            return await this._userRepos.CreateUser(newUser);
        }
        public async Task<IList<Entities.User>> GetUsers()
        {
            IList<User> users = await _userRepos.FindAllAsync(u => u.RoleId > 1);
            return users;
        }

        public async Task<string> ChangeRole1User(int id, int role)
        {
            User user = this._userRepos.GetById(id);
            if (user == null)
            {
                throw new NotFoundException("Không tìm thấy user");
            }
            if (await _roleRepos.GetByIdAsync(role) == null)
            {
                throw new NotFoundException("Không tìm thấy role");
            }
            else
            {
                user.RoleId = role;
                return await this._userRepos.changeRole1User(user);
            }
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
        public async Task<User> UpdateUser(User user, int ssId)
        {
            User dbUser = await this.GetById(user.UserId);

            if(dbUser.UserId == ssId)
            {
                dbUser.Fullname = user.Fullname;
                dbUser.PhoneNo = user.PhoneNo;
                dbUser.Email = user.Email;
                return await _userRepos.UpdateUser(dbUser);
            }
            else
            {
                throw new NotMatchException("Thông tin chỉnh sửa không khớp với thông tin đăng nhập");
            }
            
        }
        public async Task updateUserStatus(int userId)
        {
            var user = await _userRepos.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("Không tìm thấy user");
            }
            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else { user.IsActive = true; }
            await _userRepos.UpdateAsync(user, true);
        }
    }
}


