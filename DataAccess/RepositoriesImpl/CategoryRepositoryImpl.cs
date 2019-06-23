using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
    public class CategoryRepositoryImpl : GenericRepository<Categories>, ICategoryRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public CategoryRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
    }
}
