using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO.Entities;

namespace DataAccess.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<int> CreateUser(User newUser);

        Task<User> FindByUsername(string username);

        Task<IList<User>> GetUsers();
        Task<string> changeRole1User(User user);
        Task<User> UpdateUser(User user);
    }
}
