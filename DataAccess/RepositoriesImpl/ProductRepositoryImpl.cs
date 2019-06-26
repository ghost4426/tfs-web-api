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
    public class ProductRepositoryImpl : GenericRepository<Product>, IProductRepository
    {
        private FoodTrackingDbContext foodTrackerDbContext;
        private IUserRepository UserRepo;

        public ProductRepositoryImpl(FoodTrackingDbContext context, IUserRepository userRepository) : base(context)
        {
            UserRepo = userRepository;
            foodTrackerDbContext = context;
        }

        public async Task<IList<Product>> GetAllProductAsync()
        {
            return await this.GetAllAsync();
        }
        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID)
        {
            IList<Product> products = await FindAllAsync(x => x.Provider.UserId == providerID);
            IEnumerable<Product> result = products.OrderByDescending(x => x.CreatedDate).Take(500);
            return result.ToList();
        }

        public async Task<int> CreateProductAsync(Product newProduct)
        {
            newProduct.Id = 0;
            newProduct.CreatedDate = DateTime.Now;
            await this.InsertAsync(newProduct, true);
            return newProduct.Id;
        }
        public async Task<IEnumerable<Product>> GetMatchedWithNumber(int distributorId)
        {
            IList<Product> list = await this.FindAllAsync(x => x.Distributor.UserId == distributorId);
            list.OrderByDescending(x => x.CreatedDate).Take(500);
            for (int i = 0; i < list.Count; i++)
            {
                User provider = await UserRepo.FindByUserId(list.ElementAt(i).ProviderUserId);
                list.ElementAt(i).Provider = provider;
            }
            return list;
        }
    }
}