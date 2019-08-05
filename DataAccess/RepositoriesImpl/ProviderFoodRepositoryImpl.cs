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
    public class ProviderFoodRepositoryImpl : GenericRepository<ProviderFood>, IProviderFoodRepository
    {
        public ProviderFoodRepositoryImpl(FoodTrackingDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<int> createProviderFood(ProviderFood newProviderFood)
        {
            newProviderFood.CreateDate = DateTime.Now;
            await this.InsertAsync(newProviderFood, true);
            return newProviderFood.FoodId;
        }

        public async Task<IList<ProviderFood>> getAllFoodByProviderId(int providerId)
        {
            IList<ProviderFood> food = await FindAllAsync(x => x.PremisesId == providerId);
            IEnumerable<ProviderFood> result = food.OrderByDescending(x => x.CreateDate).Take(500);
            return result.ToList();
        }

        
    }
}
