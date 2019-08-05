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

        Task<int> CountFarmTransaction(int userId);

        Task<int> CountProviderTransaction(int userId);

        Task<IList<Transaction>> getAllFarmTransaction(int premisesId);

        Task<IList<Transaction>> getAllProviderReceiveTransaction(int premisesId);

        Task<Transaction> UpdateTransaction(Transaction transaction);

        Task<IList<Transaction>> getAllProviderSendTransaction(int premisesId);
    }
}
