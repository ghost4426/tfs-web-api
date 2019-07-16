﻿using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> CountFarmTransaction(int premisesId)
        {
            var transaction = await FindAllAsync(x => x.FarmId == premisesId);
            return transaction.Count;
        }

        public async Task<int> CountProviderTransaction(int premisesId)
        {
            var transaction = await FindAllAsync(x => x.ProviderId == premisesId);
            return transaction.Count;
        }

        public async Task<int> CreateSellFoodTransactionAsync(Transaction newTransaction)
        {
            newTransaction.TransactionId = 0;
            newTransaction.CreatedDate = DateTime.Now;
            newTransaction.CreatedById = 11; // sẽ đổi
            newTransaction.VeterinaryId = 11; // sẽ đổi
            newTransaction.StatusId = 1;
            await this.InsertAsync(newTransaction, true);
            return newTransaction.TransactionId;
        }

        public async Task<IList<Transaction>> getAllFarmTransaction(int premisesId)
        {
            IList<Transaction> list = await FindAllAsync(x => x.FarmId == premisesId);
            IEnumerable <Transaction> result = list.OrderByDescending(x => x.CreatedDate).Take(500);
            return result.ToList();
        }

        public async Task<IList<Transaction>> getAllProviderTransaction(int premisesId)
        {
            IList<Transaction> list = await FindAllAsync(x => x.ProviderId == premisesId);
            IEnumerable<Transaction> result = list.OrderByDescending(x => x.CreatedDate).Take(500);
            return result.ToList();
        }
    }
}
