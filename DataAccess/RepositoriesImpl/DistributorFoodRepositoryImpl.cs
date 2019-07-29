using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = DTO.Models;

namespace DataAccess.RepositoriesImpl
{
    public class DistributorFoodRepositoryImpl : GenericRepository<DistributorFood>, IDistributorFoodRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public DistributorFoodRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
        //public async Task<IList<DistributorFood>> GetDistributorFoods(int premisesId)
        //{
        //    var result = GetAllMatched(x => x.PremisesId == premisesId);
        //    return result;
        //}

        public async Task<int> createDistributorFood(DistributorFood newDistributorFood)
        {
            newDistributorFood.CreatedDate = DateTime.Now;
            await this.InsertAsync(newDistributorFood, true);
            return newDistributorFood.FoodId;
        }

        public async Task<IList<DistributorFood>> getAllFoodByDistributorId(int distributorId)
        {
            IList<DistributorFood> food = await FindAllAsync(x => x.PremisesId == distributorId);
            IEnumerable<DistributorFood> result = food.OrderByDescending(x => x.CreatedDate).Take(500);
            return result.ToList();
        }

    }
}
