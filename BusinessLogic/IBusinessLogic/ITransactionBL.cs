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

        Task<int> CountTransaction(int userId);
    }
}
