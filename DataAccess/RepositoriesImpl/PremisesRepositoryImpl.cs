using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
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

        public async Task<IList<Premises>> getAllDistributorAsync(string keyword)
        {
            IList<Premises> distribur = await FindAllAsync(x => x.TypeId == 3 && x.IsActive == true);
            IEnumerable<Premises> result = distribur.Where(x => x.Name.ToLower().Contains(keyword));
            return result.ToList();
        }

        public Task<Premises> FindByName(string premisesName)
        {
            return FindAsync(p => p.Name.Trim().ToLower() == premisesName.Trim().ToLower());
        }

        public async Task<IList<Premises>> getAllProviderAsync(string keyword)
        {
            IList<Premises> provider = await FindAllAsync(x => x.TypeId == 2 && x.IsActive == true);
            IEnumerable<Premises> result = provider.Where(x => x.Name.ToLower().Contains(keyword));
            return result.ToList();
        }
    }
}
