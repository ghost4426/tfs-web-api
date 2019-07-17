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
        private IPremesisRepository _premisesRepos;
        private IRegisterInfoRepository _registerRepos;
        public RegisterInfoBLImpl(IRegisterInfoRepository registerRepos,
            IPremisesTypeRepository premisesTypeRepos,
            IPremesisRepository premisesRepos
            )
        {
            _premisesRepos = premisesRepos;
            _premisesTypeRepos = premisesTypeRepos;
            _registerRepos = registerRepos;
        }

        public async Task<bool> CreateRegisterInfo(RegisterInfo newRegInfo)
        {
            var regInfo = await _registerRepos.FindByName(newRegInfo.PremisesName);
            if(regInfo != null)
            {   
                throw new DulicatedPremisesNameException("Tên cơ sở đã tồn tại");
            }
            //còn user manager nữa
            newRegInfo.RegisterId = 0;
            newRegInfo.CreatedDate = DateTime.Now;
            newRegInfo.IsConfirm = null;
            _registerRepos.Insert(newRegInfo, true);
            if (newRegInfo.RegisterId > 0)
            {
                return true;
            }return false;
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
        public async Task ChangeStatusRegisterInfo(int regId, int isConfirm)
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
            else { regInfo.IsConfirm = false; }

            if (regInfo.IsConfirm == true)
            {
                var isExistRegInfo = await _premisesRepos.FindByName(regInfo.PremisesName);
                if (isExistRegInfo != null)
                {
                    
                }
                else
                {
                var newPremises = new Premises();
                newPremises.Name = regInfo.PremisesName;
                newPremises.Address = regInfo.PremisesAddress;
                newPremises.CreatedDate = DateTime.Now;
                newPremises.PremisesType = _premisesTypeRepos.GetById(regInfo.PremisesTypeId);
                _premisesRepos.Insert(newPremises, true);
                }
            }
            await _registerRepos.UpdateAsync(regInfo, true);
        }
    }
}
