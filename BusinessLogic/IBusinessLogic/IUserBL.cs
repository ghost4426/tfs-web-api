using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO.Entities;
using Models = DTO.Models;

namespace BusinessLogic.IBusinessLogic
{
   public interface IUserBL
    {
        Task<int> CreateUser(User newUser);

        Task<User> CheckLogin(Models.LoginRequest loginInfo);

        Task<User> GetById(int id);

        Task<IList<User>> GetUsers();
    }
}
