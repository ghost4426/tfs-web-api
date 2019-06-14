using DTO.Entities;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface IUserBL
    {
        Task<int> CreateUser(User newUser);

    }
}
