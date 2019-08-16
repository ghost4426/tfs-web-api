using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{


    public class RegisterInfoRepositoryImpl : GenericRepository<RegisterInfo>, IRegisterInfoRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public RegisterInfoRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
        public async Task<RegisterInfo> FindByName(string premisesName)
        {
            return await FindAsync(r => r.PremisesName == premisesName);
        }
    }
}
