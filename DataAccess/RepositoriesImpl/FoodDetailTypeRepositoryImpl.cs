using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;

namespace DataAccess.RepositoriesImpl
{
    public class FoodDetailTypeRepositoryImpl : GenericRepository<FoodDetailType>, IFoodDetailTypeRepository
    {
        private FoodTrackingDbContext _dbContext;

        public FoodDetailTypeRepositoryImpl(FoodTrackingDbContext dbContext)
           : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
