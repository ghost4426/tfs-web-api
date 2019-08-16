using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
    public class FeedingFoodRepositoryImpl : GenericRepository<FeedingFood>, IFeedingFoodRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public FeedingFoodRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
    }
}
