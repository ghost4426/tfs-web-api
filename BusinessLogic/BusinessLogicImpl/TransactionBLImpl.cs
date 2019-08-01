using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using DTO.Models.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
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
                i.Farm = _premisesRepos.GetById(i.FarmId);
                i.Provider = _premisesRepos.GetById(i.ProviderId);
                i.TransactionStatus = _transactionStatusRepos.GetById(i.StatusId);
                i.Food = _foodRepos.GetById(i.FoodId);
                i.Food.Category = _categoryRepos.GetById(i.Food.CategoryId);
            }
            return transaction;
        }

        public async Task<IList<Transaction>> getAllProviderTransaction(int userId)
        {
            var transaction = await _transactionRepos.getAllProviderTransaction(userId);
            foreach (var i in transaction)
            {
                i.Farm = _premisesRepos.GetById(i.FarmId);
                i.Provider = _premisesRepos.GetById(i.ProviderId);
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
            trans.RejectedReason = transaction.RejectedReason;
            await _transactionRepos.UpdateAsync(trans, transId);
        }
        public Task<Transaction> GetTransactionById(int id)
        {
            var tran =  _transactionRepos.GetAllIncluding(t => t.Farm, t => t.Provider, t => t.Food).Where(t => t.TransactionId == id).SingleOrDefault();
            return Task.FromResult(tran);
            //dto.Farm = _premisesRepos.GetById(dto.FarmId);
            //dto.Provider = _premisesRepos.GetById(dto.ProviderId);
            //dto.Food = _foodRepos.GetById(dto.FoodId);
            //return dto;
        }
        public async Task<Transaction> UpdateTransaction(int id, int status, string reason)
        {
        
            var transaction = _transactionRepos.GetById(id);
            transaction.StatusId = status;
            if(status == 3)
            {
                transaction.VeterinaryComment = reason;
            } else
            {
                transaction.RejectedReason = reason;
            }
            
            await _transactionRepos.UpdateAsync(transaction);
            return transaction;
        }

    }
}