using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<int> CreateSellFoodTransactionAsync(Transaction newTransaction);

        Task<int> CountTransaction(int userId);
    }
}
