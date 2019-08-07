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

        Task<IList<Transaction>> getAllProviderReceiveTransaction(int premisesId);

        Task UpdateTransaction(Transaction transaction, int transId);

        Task<IList<Transaction>> getAllProviderSendTransaction(int premisesId);
    }
}
