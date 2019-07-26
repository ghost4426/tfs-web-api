using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataAccess.RepositoriesImpl
{
    public class TransactionRepositoryImpl : GenericRepository<Transaction>, ITransactionRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public TransactionRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }

      
    }
}
