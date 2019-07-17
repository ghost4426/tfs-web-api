using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = DTO.Entities;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface IRegisterInfoBL
    {
        Task CreateRegisterInfo(RegisterInfo newRegInfo);
        Task<IList<Entities.RegisterInfo>> GetAllRegisterInfo();
        Task<bool> ChangeStatusRegisterInfo(int regId, int isConfirm);
    }
}
