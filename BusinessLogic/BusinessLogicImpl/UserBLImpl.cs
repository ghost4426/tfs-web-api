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
using System.Security.Claims;
using DTO.Models.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLogic.BusinessLogicImpl
{
    public class UserBLImpl : IUserBL
    {
        private IUserRepository _userRepos;
        private IEmailSender _mailSender;
        private IRoleRepository _roleRepos;
        private IPremisesRepository _premesisRepos;
        private readonly JWTSetttings _appSettings;


        public UserBLImpl(IUserRepository userRepos
            , IEmailSender mailSender
            , IRoleRepository roleRepos
            , IPremisesRepository premesisRepos
            , IOptions<JWTSetttings> appSettings)
        {
            _mailSender = mailSender;
            _userRepos = userRepos;
            _roleRepos = roleRepos;
            _premesisRepos = premesisRepos;
            _appSettings = appSettings.Value;
        }


        public async Task<bool> CreateUser(User newUser)
        {
            var hashedPassword = PasswordHasher.GetHashPassword(newUser.Password);
            var user = await _userRepos.FindByUsername(newUser.Username);
            if (user != null)
            {
                throw new DuplicatedUsernameException(msg: MessageConstant.DUPLICATED_USERNAME);
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
        public async Task CreateVeterinary(User veterinary)
        {
            var password = Util.GeneratePassword(new Models.PasswordOptions()
            {
                RequireDigit = false,
                RequiredLength = 12,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false
            });
            var hashedPassword = PasswordHasher.GetHashPassword(password);
            var account = await _userRepos.FindByUsername(veterinary.Username);
            if (account != null)
            {
                throw new DuplicatedUsernameException(msg: MessageConstant.DUPLICATED_USERNAME);
            }
            else
            {
                veterinary.Password = hashedPassword.HashedPassword;
                veterinary.Salt = hashedPassword.Salt;
                var role = _roleRepos.GetById(4); // get veterinary Role
                veterinary.Role = role;
                _userRepos.Insert(veterinary, true);
                //Send email
                await _mailSender.SendEmailAsync(veterinary.Email, "[TFS] Tạo tài khoản thành công",
                                                "Tài khoản Thú Y của bạn đã được tạo thành công \n"
                                                +"Mật khẩu của bạn là: "+ password);
            }
        }
        public async Task ActivateAccount(string activateCode)
        {
            User user;
            user = await _userRepos.FindAsync(u => u.ActivationCode == activateCode);
            user.IsConfirmEmail = true;
            await _userRepos.UpdateAsync(user);
        }
        public async Task ChangePassword(int id, string password, string oldPass)
        {
            var user = await this._userRepos.GetByIdAsync(id);
            /*var hashedPassword = PasswordHasher.GetHashPassword(password);
            user.Password = hashedPassword.HashedPassword;
            user.Salt = hashedPassword.Salt;
            await _userRepos.UpdateUser(user);*/
            var isCorrectPassword = PasswordHasher.CheckHashedPassword(new Models.HashPassword()
            {
                HashedPassword = user.Password,
                Password = oldPass,
                Salt = user.Salt
            });
            if (isCorrectPassword)
            {
                var hashedPassword = PasswordHasher.GetHashPassword(password);
                user.Password = hashedPassword.HashedPassword;
                user.Salt = hashedPassword.Salt;
                await _userRepos.UpdateUser(user);
            }
            else throw new Exception("Mật khẩu cũ không đúng");


        }
        public async Task<IList<User>> GetUsers()
        {
            var users = await this._userRepos.FindAllAsync(u => u.RoleId > 1);
            foreach (var user in users)
            {
                var role = _roleRepos.GetById(user.RoleId);
                user.Role = role;
            }
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
        public Task<string> CheckLogin(Models.LoginRequest loginInfo)
        {
            var user = _userRepos.GetAllIncluding(u => u.Role, u => u.Premises, u => u.Premises.PremisesType).Where(u => u.Username == loginInfo.Username).SingleOrDefault();
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
                    var roles = new List<string>
                    {
                    user.Role.Name
                    };
                    string premesisId = null;
                    if (user.Premises != null)
                    {
                        roles.Add(user.Premises.PremisesType.Name);
                        premesisId = user.Premises.PremisesId.ToString();
                    }

                    ClaimsIdentity subject = new ClaimsIdentity();
                    subject.AddClaim(new Claim("userID", user.UserId.ToString()));
                    if (premesisId != null)
                    {
                        subject.AddClaim(new Claim("premisesID", premesisId));
                    }
                    foreach (var role in roles)
                    {
                        subject.AddClaim(new Claim(ClaimTypes.Role, role));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {

                        Subject = subject,
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Task.FromResult(token);
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
        public async Task ChangeAvatar(int userId, string avaUrl)
        {
            var user = _userRepos.GetById(userId);
            user.Image = avaUrl;
            await _userRepos.UpdateAsync(user);
        }
        public async Task<User> UpdateUser(User user, int ssId)
        {
            User dbUser = await this.GetById(user.UserId);

            if (dbUser.UserId == ssId)
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

        public Task<User> FindByName(string username)
        {
            var user = _userRepos.GetAllIncluding(u => u.Role, u => u.Premises, u => u.Premises.PremisesType).Where(u => u.Username == username).SingleOrDefault();
            return Task.FromResult(user);
        }

        public async Task<IList<User>> getUsersByPremises(int premisesId)
        {
            var users = await this._userRepos.FindAllAsync(u => u.PremisesId == premisesId & u.RoleId == 3);
            foreach (var user in users)
            {
                var role = _roleRepos.GetById(user.RoleId);
                user.Role = role;
            }
            return users.Take(500).ToList();
        }
    }
}


