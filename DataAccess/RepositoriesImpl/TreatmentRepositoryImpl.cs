using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
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
            return await FindAllAsync(x => x.TreatmentId == treatmentId | x.TreatmentParentId == treatmentId);
        }
    }
}
