using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{


    public class CategoryRepositoryImpl : GenericRepository<Category>, ICategoryRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public CategoryRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await FindAsync(x => x.CategoryId == id);
        }

    }
}
