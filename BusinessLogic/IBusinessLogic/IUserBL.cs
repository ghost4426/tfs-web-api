﻿using System;
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

        Task<bool> CreateAdmin(User newUser);

        Task<string> CheckLogin(Models.LoginRequest loginInfo);

        Task<User> GetById(int id);

        Task<IList<User>> GetUsers();

        Task CreateVeterinary(User newVeterinary);

        Task RemoveByIdAsync(int id);

        Task<string> ChangeRole1User(int id, int role);

        Task<User> UpdateUser(User user, int ssId);

        Task ChangeAvatar(int userId, string avaUrl);
        Task ActivateAccount(string activateCode);

        Task updateUserStatus(int userId);

        Task<bool> Register(User user, Premises premises);

        Task ChangePassword(int id, string password, string oldPass);

        Task<User> FindByName(string username);

        Task<IList<User>> getUsersByPremises(int premisesId);

        Task resetPassword(string email);
    }
}
