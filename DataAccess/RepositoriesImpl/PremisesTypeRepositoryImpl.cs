using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.RepositoriesImpl
{
    public class PremisesTypeRepositoryImpl : GenericRepository<PremisesType>, IPremisesTypeRepository
    {
        private FoodTrackingDbContext _dbContext;

        public PremisesTypeRepositoryImpl(FoodTrackingDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
