using Entities=DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface IRoleBL
    {
        Task<Entities.Role> GetById(int id);
        Task<IList<Entities.Role>> GetAllRole();
    }
}
