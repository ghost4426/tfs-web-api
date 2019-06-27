using DataAccess.Context;
using DataAccess.IRepositories;
using DTO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = DTO.Models;

namespace DataAccess.RepositoriesImpl
{
    public class MaterialRepositoryImpl : GenericRepository<Material>, IMaterialRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;

        private IUserRepository UserRepo;
        public MaterialRepositoryImpl(FoodTrackingDbContext context, IUserRepository userRepository) : base(context)
        {
            UserRepo = userRepository;
            foodTrackerDbContext = context;
        }
        public async Task<IEnumerable<Material>> GetMaterialByFarmerId(int FarmerId)
        {
            IList<Material> list = await this.FindAllAsync(x => x.Farmer.UserId == FarmerId);
            list.OrderByDescending(x => x.CreatedDate).Take(500);
            for (int i = 0; i < list.Count; i++)
            {
                User provider = UserRepo.GetById(list.ElementAt(i).ProviderId.Value);
                list.ElementAt(i).Provider = provider;
            }
            return list;
        }
        
    }
}