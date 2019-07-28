using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.RepositoriesImpl
{
    public class TransactionStatusRepositoryImpl:GenericRepository<TransactionStatus>,ITransactionStatusRepository
    {
        public TransactionStatusRepositoryImpl(FoodTrackingDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
