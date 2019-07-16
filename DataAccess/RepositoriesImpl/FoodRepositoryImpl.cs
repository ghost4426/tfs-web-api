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
    public class FoodRepositoryImpl : GenericRepository<Food>, IFoodRepository
    {
        private IUserRepository UserRepo;

        public FoodRepositoryImpl(FoodTrackingDbContext _dbContext, IUserRepository userRepository) : base(_dbContext)
        {
            UserRepo = userRepository;
        }

        public async Task<IList<Food>> GetAllProductAsync()
        {
            return await this.GetAllAsync();
        }
        public async Task<IList<Food>> FindAllProductByProviderAsync(int providerID)
        {
            //IList<Food> products = await FindAllAsync(x => x.ProviderId == providerID);
            //IEnumerable<Food> result = products.OrderByDescending(x => x.CreatedDate).Take(500);
            //return result.ToList();
            return null;
        }

        public async Task<int> CreateProductAsync(Food newProduct)
        {
            newProduct.FoodId = 0;
            newProduct.IsCertification = false;
            newProduct.IsFeeding = false;
            newProduct.IsPackaging = false;
            newProduct.IsTreatment = false;
            newProduct.IsVaccination = false;
            newProduct.CreatedDate = DateTime.Now;
            await this.InsertAsync(newProduct, true);
            return newProduct.FoodId;
        }
        public async Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID)
        {
            IList<Food> products = await FindAllAsync(x => x.FarmId == farmerID);
            IEnumerable<Food> result = products.OrderByDescending(x => x.CreatedDate).Take(500);
            return result.ToList();
        }


        public async Task<Food> GetProductByIdAsync(int foodID)
        {
            Food product = await FindAsync(x => x.FoodId == foodID);
            return product;
        }
    }
}