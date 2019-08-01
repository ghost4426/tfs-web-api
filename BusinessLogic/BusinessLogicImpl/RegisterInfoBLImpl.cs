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
    public class RegisterInfoBLImpl : IRegisterInfoBL
    {
        private IPremisesTypeRepository _premisesTypeRepos;
        private IPremisesRepository _premisesRepos;
        private IRegisterInfoRepository _registerRepos;
        private IUserRepository _userRepository;
        private IEmailSender _mailSender;
        private IRoleRepository _roleRepos;
        public RegisterInfoBLImpl(IRegisterInfoRepository registerRepos,
            IPremisesTypeRepository premisesTypeRepos,
            IPremisesRepository premisesRepos,
            IUserRepository userRepository,
            IEmailSender mailSender,
            IRoleRepository roleRepos
            )
        {
            _roleRepos = roleRepos;
            _mailSender = mailSender;
            _userRepository = userRepository;
            _premisesRepos = premisesRepos;
            _premisesTypeRepos = premisesTypeRepos;
            _registerRepos = registerRepos;
        }

        public async Task CreateRegisterInfo(RegisterInfo newRegInfo)
        {
            var regInfo = await _registerRepos.FindByName(newRegInfo.PremisesName);
            var isExistUser = await _userRepository.FindByUsername(newRegInfo.Username);
            if (regInfo != null)
            {   
                throw new DuplicatedPremisesNameException("Tên cơ sở đã tồn tại");
            }else if(isExistUser!= null)
            {
                throw new DuplicatedUsernameException("Tài khoản đã tồn tại");
            }
            //còn user manager nữa
            else {
                newRegInfo.RegisterId = 0;
                newRegInfo.IsConfirm = null;
                _registerRepos.Insert(newRegInfo, true);
            }
        }
        public async Task<IList<Entities.RegisterInfo>> GetAllRegisterInfo()
        {
            var regInfos = await _registerRepos.FindAllAsync(p => p.RegisterId > 0);
            foreach(var reg in regInfos)
            {
                var premises = _premisesTypeRepos.GetById(reg.PremisesTypeId);
                reg.PremisesType = premises;
            }
            return regInfos;
        }
        public async Task<bool> ChangeStatusRegisterInfo(int regId, int isConfirm)
        {
            var regInfo = await _registerRepos.GetByIdAsync(regId);
            if(isConfirm == 1)
            {
                regInfo.IsConfirm = true;
            }
            else if(isConfirm == 2)
            {
                regInfo.IsConfirm = null;
            }
            else {
                regInfo.IsConfirm = false;
            }

            if (regInfo.IsConfirm == true)
            {
                    var newPremises = new Premises();
                    newPremises.Name = regInfo.PremisesName;
                    newPremises.Address = regInfo.PremisesAddress;
                    newPremises.PremisesType = _premisesTypeRepos.GetById(regInfo.PremisesTypeId);
                    var user = new User();
                    var password = Util.GeneratePassword(new Models.PasswordOptions()
                    {
                        RequireDigit = true,
                        RequiredLength = 8,
                        RequireLowercase = true,
                        RequireNonAlphanumeric = false,
                        RequireUppercase = true
                    });
                    var hashedPassword = PasswordHasher.GetHashPassword(password);//Get hashedpassword
                    var role = _roleRepos.GetById(2);//Get manager Role
                    user.Password = hashedPassword.HashedPassword;
                    user.Fullname = regInfo.Fullname;
                    user.Salt = hashedPassword.Salt;
                    user.Role = role;
                    user.Username = regInfo.Username;
                    user.Email = regInfo.Email;
                    user.Image = "/app-assets/images/avatar.jpg";
                    user.Premises = newPremises;
                    _userRepository.Insert(user, true);
                    await _mailSender.SendEmailAsync(user.Email, "Thông tin cơ sở của bạn đã dc duyệt", "Mật khẩu tài khoản: " + password);
                    return true;
            }
            await _registerRepos.UpdateAsync(regInfo, true);
            return false;
        }
    }
}
