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
    public class VaccineRepositoryImpl : GenericRepository<Vaccine>, IVaccineRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        public VaccineRepositoryImpl(FoodTrackingDbContext context) : base(context)
        {
            foodTrackerDbContext = context;
        }
    }
}
