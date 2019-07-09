using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class ProductBLImpl : IProductBL
    {
        private IFoodRepository _foodRepos;
        private ICategoryRepository _categoryRepos;

        public ProductBLImpl(IFoodRepository productRepos, ICategoryRepository categoryRepos)
        {
            _foodRepos = productRepos;
            _categoryRepos = categoryRepos;
        }

        public async Task<IList<Food>> GetAllProductAsync()
        {
            return await this._foodRepos.GetAllAsync();
        }

        public async Task<IList<Food>> FindAllProductByProviderAsync(int providerID)
        {
            var products = await this._foodRepos.FindAllProductByProviderAsync(providerID);
            foreach (var product in products)
            {
                var cat = _categoryRepos.GetById(product.Category);
                product.Category = cat;
            }
            return products;
        }

        public async Task<int> CreateProductAsync(Food newProduct)
        {
            return await this._foodRepos.CreateProductAsync(newProduct);
        }

        public async Task<IEnumerable<Food>> getMatchedWithNumber(int distributorId)
        {
            return await this._foodRepos.GetMatchedWithNumber(distributorId);
        }

        public async Task<IList<Category>> getAllCategory()
        {
            return await this._categoryRepos.GetAllAsync();
        }

        public async Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID)
        {
            var products = await this._foodRepos.FindAllProductByFarmerAsync(farmerID);
            foreach (var product in products)
            {
                var cat = _categoryRepos.GetById(product.CategoryId);
                product.Category = cat;
            }
            return products;
        }

        public async Task<Food> FindProductById(int foodID)
        {
            var product = await this._foodRepos.GetProductByIdAsync(foodID);
            var cat = _categoryRepos.GetById(product.Category);
            product.Category = cat;
            return product;
        }
    }
}
