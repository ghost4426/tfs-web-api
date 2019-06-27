using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class ProductBLImpl : IProductBL
    {
        private IProductRepository _productRepos;
        private ICategoryRepository _categoryRepos;

        public ProductBLImpl(IProductRepository productRepos, ICategoryRepository categoryRepos)
        {
            _productRepos = productRepos;
            _categoryRepos = categoryRepos;
        }

        public async Task<IList<Product>> GetAllProductAsync()
        {
            return await this._productRepos.GetAllAsync();
        }

        public async Task<IList<Product>> FindAllProductByProviderAsync(int providerID)
        {
            var products = await this._productRepos.FindAllProductByProviderAsync(providerID);
            foreach (var product in products)
            {
                var cat = _categoryRepos.GetById(product.CategoriesId);
                product.Categories = cat;
            }
            return products;
        }

        public async Task<int> CreateProductAsync(Product newProduct)
        {
            return await this._productRepos.CreateProductAsync(newProduct);
        }

        public async Task<IEnumerable<Product>> getMatchedWithNumber(int distributorId)
        {
            return await this._productRepos.GetMatchedWithNumber(distributorId);
        }

        public async Task<IList<Categories>> getAllCategory()
        {
            return await this._categoryRepos.GetAllAsync();
        }
    }
}
