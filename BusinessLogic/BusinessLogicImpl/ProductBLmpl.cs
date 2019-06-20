using BusinessLogic.IBusinessLogic;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using DataAccess.IRepositories;

namespace BusinessLogic.BusinessLogicImpl
{
    public class ProductBLmpl : IProductBL
    {
        private IProductRepository repos;

        public ProductBLmpl(IProductRepository productRepository)
        {
            if(productRepository != null)
            {
                this.repos = productRepository;
            }
        }

        public async Task<int> CreateProductAsync(Product newProduct)
        {
            return await this.repos.CreateProductAsync(newProduct);
        }

        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID)
        {
            return await this.repos.FindAllProductByProviderAsync(providerID);
        }

        public async Task<IList<Product>> GetAllProduct()
        {
            return await this.repos.GetAllAsync();
        }

        public async Task<IList<Product>> GetAllProductAsync()
        {
            return await this.repos.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await this.repos.GetMatchedWithNumber(distributorId);
        }
    }
}
