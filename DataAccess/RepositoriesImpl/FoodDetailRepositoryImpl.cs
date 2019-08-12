using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;

namespace DataAccess.RepositoriesImpl
{
    public class FoodDetailRepositoryImpl : GenericRepository<FoodDetail>, IFoodDetailRepository
    {
        private FoodTrackingDbContext _dbContext;

        public FoodDetailRepositoryImpl(FoodTrackingDbContext dbContext)
           : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
