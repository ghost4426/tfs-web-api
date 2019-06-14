using DTO.Entities;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<int> CreateUserAsync(User newUser);

    }
}
