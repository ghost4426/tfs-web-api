using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImpl
{
    public class RoleRepositoryImpl : GenericRepository<Role>, IRoleRepository
    {

        private readonly FoodTrackingDbContext contex;

        public RoleRepositoryImpl(FoodTrackingDbContext context)
           : base(context)
        {
            contex = context;
        }


    }
}
