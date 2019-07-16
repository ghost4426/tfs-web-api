using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class TransactionBLImpl : ITransactionBL
    {
        private ITransactionRepository _transactionRepos;

        public TransactionBLImpl(ITransactionRepository transactionRepos)
        {
            _transactionRepos = transactionRepos;
        }

        public async Task<int> CountTransaction(int userId)
        {
            return await _transactionRepos.CountTransaction(userId);
        }

        public async Task<int> CreateSellFoodTransactionAsync(Transaction newTransaction)
        {
            return await this._transactionRepos.CreateSellFoodTransactionAsync(newTransaction);
        }
    }
}
