using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
   public interface IRoleBL
    {
        Task<Role> GetById(int id);
    }
}
