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
using Entities = DTO.Entities;


namespace BusinessLogic.BusinessLogicImpl
{
    public class UserBLImpl : IUserBL
    {
        private IUserRepository _userRepos;
        private IRoleRepository _roleRepos;
        private IPremisesRepository _premesisRepos;

        public UserBLImpl(IUserRepository userRepos, IRoleRepository roleRepos, IPremisesRepository premesisRepos)
        {
            _userRepos = userRepos;
            _roleRepos = roleRepos;
            _premesisRepos = premesisRepos;
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
            _userRepos.Insert(newUser, true);
            if (newUser.UserId > 0)
            {
                return true;
            }
            return false;
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

        public Task Register(User user, Premises premises)
        {
            throw new NotImplementedException();
        }

        //public Task Register(User user, Premises premises)
        //{
        //    _userRepos.Insert(user);
        //    _premesisRepos.Insert(premises);
        //}
    }
}


