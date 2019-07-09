using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
    public class TransactionRepositoryImpl : GenericRepository<Transaction>, ITransactionRepository
    {
        private IUserRepository UserRepo;

        public TransactionRepositoryImpl(FoodTrackingDbContext _dbContext, IUserRepository userRepository) : base(_dbContext)
        {
            UserRepo = userRepository;
        }
        public async Task<int> CreateSellFoodTransactionAsync(Transaction newTransaction)
        {
            newTransaction.TransactionId = 0;
            newTransaction.CreatedDate = DateTime.Now;
            newTransaction.ConfirmDate = DateTime.Now;
            await this.InsertAsync(newTransaction, true);
            return newTransaction.TransactionId;
        }
    }
}
