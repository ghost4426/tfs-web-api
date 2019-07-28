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
    }
}
