using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO.Entities;

namespace DataAccess.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<int> CreateUserAsync(User newUser);

    }
}
