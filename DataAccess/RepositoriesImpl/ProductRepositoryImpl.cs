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
    public class ProductRepositoryImpl : GenericRepository<Food>, IProductRepository
    {
        private IUserRepository UserRepo;

        public ProductRepositoryImpl(FoodTrackingDbContext _dbContext, IUserRepository userRepository) : base(_dbContext)
        {
            UserRepo = userRepository;
        }

        public async Task<IList<Food>> GetAllProductAsync()
        {
            return await this.GetAllAsync();
        }
        public async Task<IList<Food>> FindAllProductByProviderAsync(int providerID)
        {
            IList<Food> products = await FindAllAsync(x => x.Provider.PremisesId == providerID);
            IEnumerable<Food> result = products.OrderByDescending(x => x.CreatedDate).Take(500);
            return result.ToList();
        }

        public async Task<int> CreateProductAsync(Food newProduct)
        {
            //    newProduct.Id = 0;
            //    newProduct.CreatedDate = DateTime.Now;
            //    await this.InsertAsync(newProduct, true);
            //    return newProduct.Id;
            return 0;
        }
        public async Task<IEnumerable<Food>> GetMatchedWithNumber(int distributorId)
        {
            ////IList<Food> list = await this.FindAllAsync(x => x.DistributorFoods.Contains(distributorId == distributorId);
            //list.OrderByDescending(x => x.CreatedDate).Take(500);
            ////for (int i = 0; i < list.Count; i++)
            ////{
            ////    var provider = await UserRepo.GetByIdAsync(list.ElementAt(i).ProviderUserId);
            ////    list.ElementAt(i).Provider = provider;
            ////}
            //return list;
            return null;
        }
    }
}