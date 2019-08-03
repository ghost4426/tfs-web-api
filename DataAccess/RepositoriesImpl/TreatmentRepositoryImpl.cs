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
   public class TreatmentRepositoryImpl : GenericRepository<Treatment>, ITreatmentRepository
    {
        private FoodTrackingDbContext _dbContext;

        public TreatmentRepositoryImpl(FoodTrackingDbContext dbContext)
           : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Treatment>> getAllTreatmentById(int treatmentId)
        {
            return await FindAllAsync(x =>  x.TreatmentParentId == treatmentId);
        }

        public async Task<IList<Treatment>> getAllTreatmentByPremisesId(int premisesId)
        {
            return await FindAllAsync(x => x.PremisesId == premisesId & x.TreatmentParentId == null);
        }

        public async Task<IList<int>> getTreatmentIdByParent(int treatmentId)
        {
            IList<Treatment> treatment = await FindAllAsync(x => x.TreatmentParentId == treatmentId);
            IEnumerable<int> result = treatment.Select(s =>  s.TreatmentId );
            return result.ToList();
        }
    }
}
