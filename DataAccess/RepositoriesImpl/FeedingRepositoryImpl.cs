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
    public class FeedingRepositoryImpl : GenericRepository<Feeding>, IFeedingRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public FeedingRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
    }
}
