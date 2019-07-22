using BusinessLogic.IBusinessLogic;
using Common.Enum;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodBLImpl : IFoodBL
    {
        private IFoodRepository _productRepos;
        private ICategoryRepository _categoryRepos;
        private IDistributorFoodRepository _distributorFoodRepository;
        private IProviderFoodRepository _providerFoodRepository;

        public FoodBLImpl(IFoodRepository productRepos, ICategoryRepository categoryRepos, IDistributorFoodRepository distributorFoodRepository, IProviderFoodRepository providerFoodRepository)
        {
            _productRepos = productRepos;
            _categoryRepos = categoryRepos;
            _distributorFoodRepository = distributorFoodRepository;
            _providerFoodRepository = providerFoodRepository;
        }

        public async Task<IList<Food>> GetAllProductAsync()
        {
            return await this._productRepos.GetAllAsync();
        }

        public async Task<IList<Food>> FindAllProductByProviderAsync(int providerID)
        {
            var products = await this._productRepos.FindAllProductByProviderAsync(providerID);
            foreach (var product in products)
            {
                var cat = _categoryRepos.GetById(product.CategoryId);
                product.Category = cat;
            }
            return products;
        }

        public async Task<int> CreateProductAsync(Food newProduct)
        {
            return await this._productRepos.CreateProductAsync(newProduct);
        }

        public async Task<IList<Food>> getMatchedWithNumber(int distributorId)
        {
            //var products = await this._productRepos.GetMatchedWithNumber(distributorId);
            //foreach (var product in products)
            //{
            //    var provider = _premesisRepository.GetById(product.ProviderId);
            //    product.Provider = provider;
            //}
            //return products;
            return null;
        }

        public async Task<IList<Category>> getAllCategory()
        {
            return await this._categoryRepos.GetAllAsync();
        }

        public async Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID)
        {
            var products = await this._productRepos.FindAllProductByFarmerAsync(farmerID);
            foreach (var product in products)
            {
                var cat = _categoryRepos.GetById(product.CategoryId);
                product.Category = cat;
            }
            return products;
        }

        public async Task AddDetail(long foodId, EFoodDetailType type)
        {
            var food = _productRepos.GetById((int)foodId);
            switch (type)
            {
                case EFoodDetailType.FEEDING:
                    food.IsFeeding = true;
                    break;
                case EFoodDetailType.VACCINATION:
                    food.IsVaccination = true;
                    break;
                case EFoodDetailType.CERTIFICATION:
                    food.IsCertification = true;
                    break;
                case EFoodDetailType.TREATMENT:
                    food.IsTreatment = true;
                    break;
                case EFoodDetailType.PACKAGING:
                    food.IsPackaging = true;
                    break;
                default: break;
            }
            await _productRepos.UpdateAsync(food);
        }

        public async Task<IList<ProviderFood>> getAllFoodByProviderId(int providerId)
        {
            var food = await _providerFoodRepository.getAllFoodByProviderId(providerId);
            foreach(var i in food)
            {
                i.Food = _productRepos.GetById(i.FoodId);
                i.Food.Category = _categoryRepos.GetById(i.Food.CategoryId);
            }
            return food;
        }

        public async Task<int> createProviderFood(ProviderFood newProviderFood)
        {
            return await _providerFoodRepository.createProviderFood(newProviderFood);
        }

        public async Task UpdateFoodTreatment(Food food, int foodId)
        {
            Food result = _productRepos.GetById(food.FoodId);
            result.TreatmentId = food.TreatmentId;
            await _productRepos.UpdateAsync(result, foodId);
        }

        public async Task<Food> getFoodById(int foodId)
        {
            return await _productRepos.GetByIdAsync(foodId);
        }
    }
}
