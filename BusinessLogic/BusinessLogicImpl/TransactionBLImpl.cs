using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using DTO.Models.Exception;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class TransactionBLImpl : ITransactionBL
    {
        private ITransactionRepository _transactionRepos;
        private IPremisesRepository _premisesRepos;
        private ITransactionStatusRepository _transactionStatusRepos;
        private IFoodRepository _foodRepos;
        private ICategoryRepository _categoryRepos;

        public TransactionBLImpl(ITransactionRepository transactionRepos,
            IPremisesRepository premisesRepos,
            ITransactionStatusRepository transactionStatus,
            IFoodRepository foodRepos,
            ICategoryRepository categoryRepos)
        {
            _transactionRepos = transactionRepos;
            _premisesRepos = premisesRepos;
            _transactionStatusRepos = transactionStatus;
            _foodRepos = foodRepos;
            _categoryRepos = categoryRepos;
        }

        public async Task<int> CountFarmTransaction(int userId)
        {
            return await _transactionRepos.CountFarmTransaction(userId);
        }

        public async Task<int> CountProviderTransaction(int userId)
        {
            return await _transactionRepos.CountProviderTransaction(userId);
        }

        public async Task<int> CreateSellFoodTransactionAsync(Transaction newTransaction)
        {
            return await this._transactionRepos.CreateSellFoodTransactionAsync(newTransaction);
        }

        public async Task<IList<Transaction>> getAllFarmTransaction(int userId)
        {
            var transaction = await _transactionRepos.getAllFarmTransaction(userId);
            foreach(var i in transaction)
            {
                i.Sender = _premisesRepos.GetById(i.SenderId);
                i.Receiver = _premisesRepos.GetById(i.ReceiverId);
                i.TransactionStatus = _transactionStatusRepos.GetById(i.StatusId);
                i.Food = _foodRepos.GetById(i.FoodId);
                i.Food.Category = _categoryRepos.GetById(i.Food.CategoryId);
            }
            return transaction;
        }

        public async Task<IList<Transaction>> getAllProviderReceiveTransaction(int userId)
        {
            var transaction = await _transactionRepos.getAllProviderReceiveTransaction(userId);
            foreach (var i in transaction)
            {
                i.Sender = _premisesRepos.GetById(i.SenderId);
                i.Receiver = _premisesRepos.GetById(i.ReceiverId);
                i.TransactionStatus = _transactionStatusRepos.GetById(i.StatusId);
                i.Food = _foodRepos.GetById(i.FoodId);
                i.Food.Category = _categoryRepos.GetById(i.Food.CategoryId);
            }
            return transaction;
        }

        public async Task<IList<Transaction>> getAllProviderSendTransaction(int premisesId)
        {
            var transaction = await _transactionRepos.getAllProviderSendTransaction(premisesId);
            foreach (var i in transaction)
            {
                i.Sender = _premisesRepos.GetById(i.SenderId);
                i.Receiver = _premisesRepos.GetById(i.ReceiverId);
                i.TransactionStatus = _transactionStatusRepos.GetById(i.StatusId);
                i.Food = _foodRepos.GetById(i.FoodId);
                i.Food.Category = _categoryRepos.GetById(i.Food.CategoryId);
            }
            return transaction;
        }

        public async Task UpdateTransaction(Transaction transaction, int transId)
        {
            Transaction trans = _transactionRepos.GetById(transaction.TransactionId);
            trans.StatusId = transaction.StatusId;
            trans.RejectReason = transaction.RejectReason;
            trans.ReceiverComment = transaction.ReceiverComment;
            await _transactionRepos.UpdateAsync(trans, transId);
        }
    }
}
