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
        private IFoodRepository _productRepos;

        public TransactionBLImpl(ITransactionRepository transactionRepos,
            IPremisesRepository premisesRepos,
            ITransactionStatusRepository transactionStatus,
            IFoodRepository foodRepos,
            ICategoryRepository categoryRepos,
            IFoodRepository foodRepository)
        {
            _transactionRepos = transactionRepos;
            _premisesRepos = premisesRepos;
            _transactionStatusRepos = transactionStatus;
            _foodRepos = foodRepos;
            _categoryRepos = categoryRepos;
            _productRepos = foodRepository;
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
            var food = _foodRepos.GetById(newTransaction.FoodId);
            food.IsReadyForSale = true;
            await _foodRepos.UpdateAsync(food);
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
        public Task<Transaction> GetTransactionById(int id)
        {
            var tran =  _transactionRepos.GetAllIncluding(t => t.Sender, t => t.Receiver, t => t.Food).Where(t => t.TransactionId == id).SingleOrDefault();
            return Task.FromResult(tran);
            //dto.Farm = _premisesRepos.GetById(dto.FarmId);
            //dto.Provider = _premisesRepos.GetById(dto.ProviderId);
            //dto.Food = _foodRepos.GetById(dto.FoodId);
            //return dto;
        }
        public async Task<Transaction> UpdateVerterinaryTransaction(int id, int status, string reason, int verId)
        {
        
            var transaction = _transactionRepos.GetById(id);
            transaction.StatusId = status;
            if(status == 2)
            {
                transaction.VeterinaryComment = reason;
            } else
            {
                transaction.RejectReason = reason;
                transaction.RejectById = verId;
            }
            transaction.VeterinaryId = verId;
            await _transactionRepos.UpdateAsync(transaction);
            return transaction;
        }

        public async Task<Transaction> UpdateDistributorTransaction(int id, int status, string reasone, int distributorId)
        {
            var transaction = _transactionRepos.GetById(id);
            transaction.StatusId = status;
            if(status == 3)
            {
                transaction.ReceiverComment = reasone;
            } else
            {
                transaction.RejectReason = reasone;
                transaction.RejectById = distributorId;
            }
            await _transactionRepos.UpdateAsync(transaction);
            return transaction;
        }

        public async Task<IList<Transaction>> ProviderFoodInReport(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _transactionRepos.FindAllAsync(x => x.ReceiverId == premisesId && x.CreateDate.Month == month && x.StatusId == 3);
            foreach (var t in result)
            {
                t.Food = _productRepos.GetById(t.FoodId);
                t.Food.Category = _categoryRepos.GetById(t.Food.CategoryId);
                t.Sender = _premisesRepos.GetById(t.SenderId);
            }
            return result;
        }

        public async Task<IList<Transaction>> ProviderFoodOutReport(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _transactionRepos.FindAllAsync(x => x.SenderId == premisesId && x.CreateDate.Month == month && x.StatusId == 3);
            foreach (var t in result)
            {
                t.Food = _productRepos.GetById(t.FoodId);
                t.Food.Category = _categoryRepos.GetById(t.Food.CategoryId);
                t.Receiver = _premisesRepos.GetById(t.ReceiverId);
            }
            return result;
        }

        public async Task<IList<Transaction>> ProviderFoodRejectReport(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _transactionRepos.FindAllAsync(x => x.SenderId == premisesId && x.CreateDate.Month == month && x.StatusId == 4);
            foreach (var t in result)
            {
                t.Food = _productRepos.GetById(t.FoodId);
                t.Food.Category = _categoryRepos.GetById(t.Food.CategoryId);
                t.Receiver = _premisesRepos.GetById(t.ReceiverId);
            }
            return result;
        }
    }
}