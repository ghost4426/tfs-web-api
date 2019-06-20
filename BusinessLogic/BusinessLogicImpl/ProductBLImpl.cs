using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class ProductBLImpl : IProductBL
    {
        private IProductRepository repos;

        public ProductBLImpl(IProductRepository productRepository)
        {
            if (productRepository != null)
                this.repos = productRepository;
        }        

        public async Task<IList<Product>> GetAllProductAsync()
        {
            return await this.repos.GetAllAsync();
        }

        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID)
        {
            return await this.repos.FindAllProductByProviderAsync(providerID);
        }

        public async Task<int> CreateProductAsync(Product newProduct)
        {
            return await this.repos.CreateProductAsync(newProduct);
        }

        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await this.repos.GetMatchedWithNumber(distributorId);
        }
    }
}
