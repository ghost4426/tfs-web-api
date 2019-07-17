using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
   public class PremisesRepositoryImpl : GenericRepository<Premises>, IPremesisRepository
    {
        private FoodTrackingDbContext _dbContext;

        public PremisesRepositoryImpl(FoodTrackingDbContext dbContext)
           : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Premises> FindByName(string premisesName)
        {
            return await FindAsync(p => p.Name == premisesName);
        }

    }
}
