using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
