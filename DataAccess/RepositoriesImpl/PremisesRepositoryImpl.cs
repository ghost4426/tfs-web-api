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
    public class PremisesRepositoryImpl : GenericRepository<Premises>, IPremisesRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public PremisesRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
        public async Task<IList<Premises>> getAllProviderAsync(string keyword)
        {
            IList<Premises> provider = await FindAllAsync(x => x.TypeId == 2);
            IEnumerable<Premises> result = provider.Where(x => x.Name.ToLower().Contains(keyword));
            return result.ToList();
        }
    }
}
