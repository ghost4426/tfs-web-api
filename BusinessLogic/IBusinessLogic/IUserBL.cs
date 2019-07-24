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
        Task<bool> CreateUser(User newUser);

        Task<string> CheckLogin(Models.LoginRequest loginInfo);

        Task<User> GetById(int id);

        Task<IList<User>> GetUsers();

        Task RemoveByIdAsync(int id);

        Task<string> ChangeRole1User(int id, int role);

        Task<User> UpdateUser(User user,int ssId);

        Task updateUserStatus(int userId);

        Task Register(User user, Premises premises);

    }
}
