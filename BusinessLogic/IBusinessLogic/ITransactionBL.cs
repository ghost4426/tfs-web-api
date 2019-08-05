using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBusinessLogic
{
    public interface ITransactionBL
    {
        Task<int> CreateSellFoodTransactionAsync(Transaction newTransaction);

        Task<int> CountFarmTransaction(int premisesId);

        Task<int> CountProviderTransaction(int premisesId);

        Task<IList<Transaction>> getAllFarmTransaction(int premisesId);

        Task<IList<Transaction>> getAllProviderTransaction(int premisesId);

        Task UpdateTransaction(Transaction transaction, int transId);
        Task<Transaction> UpdateTransaction(int id, int status, string reasone, int verId);

        Task<Transaction> GetTransactionById(int id);
    }
}