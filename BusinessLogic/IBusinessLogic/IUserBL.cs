using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO.Entities;

namespace BusinessLogic.IBusinessLogic
{
   public interface IUserBL
    {
        Task<int> CreateUser(User newUser);

    }
}
