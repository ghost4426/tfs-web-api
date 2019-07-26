using BusinessLogic.IBusinessLogic;
using Common.Enum;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class TransactionBLImpl: ITransactionBL
    {
        private ITransactionRepository _transactionRepository;
        private IPremesisRepository _premesisRepository;
        private IFoodRepository _foodRepository;

        public TransactionBLImpl(ITransactionRepository transactionRepository, 
            IPremesisRepository premesisRepository,
            IFoodRepository foodRepository)
        {
            _transactionRepository = transactionRepository;
            _premesisRepository = premesisRepository;
            _foodRepository = foodRepository;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            var dto =  _transactionRepository.GetById(id);
            dto.Farm = _premesisRepository.GetById(dto.FarmId);
            dto.Provider = _premesisRepository.GetById(dto.ProviderId);
            dto.Food = _foodRepository.GetById(dto.FoodId);
            return dto;
        }
        
        public async Task<Transaction> UpdateTransaction(int id, int status, string reasone)
        {
            var transaction = _transactionRepository.GetById(id);
            transaction.StatusId = status;
            transaction.RejectedReason = reasone;
            await _transactionRepository.UpdateAsync(transaction);
            return transaction;
        }

    }
}
