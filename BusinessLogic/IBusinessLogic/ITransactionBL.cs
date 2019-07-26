using Common.Enum;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface ITransactionBL
    {
        Task<Transaction> UpdateTransaction(int id, int status, string reasone);

        Task<Transaction> GetTransactionById(int id);
    }
}
