using BusinessLogic.IBusinessLogic;
using Common.Constant;
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
        private readonly IFoodRepository _productRepos;
        private readonly ICategoryRepository _categoryRepos;
        private readonly IDistributorFoodRepository _distributorFoodRepository;
        private readonly IProviderFoodRepository _providerFoodRepository;
        private readonly IFoodDetailRepository _foodDetailRepository;

        public FoodBLImpl(
            IFoodRepository productRepos
            , ICategoryRepository categoryRepos
            , IDistributorFoodRepository distributorFoodRepository
            , IProviderFoodRepository providerFoodRepository
            , IFoodDetailRepository foodDetailRepository
            )
        {
            _productRepos = productRepos;
            _categoryRepos = categoryRepos;
            _distributorFoodRepository = distributorFoodRepository;
            _providerFoodRepository = providerFoodRepository;
            _foodDetailRepository = foodDetailRepository;
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

        public async Task AddDetail(int foodId, EFoodDetailType type, string transactionHash, int userID)
        {
            var foodDetail = new FoodDetail()
            {
                TransactionHash = transactionHash,
                FoodId = foodId,
                CreateById = userID
            };
            switch (type)
            {
                case EFoodDetailType.CREATE:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.CREATE_NEW_ID;
                    break;
                case EFoodDetailType.FEEDING:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.ADD_FEEDING_ID;
                    break;
                case EFoodDetailType.VACCINATION:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.ADD_VACCINATION_ID;
                    break;
                case EFoodDetailType.VERIFY:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.ADD_VERIFY_ID;
                    break;
                case EFoodDetailType.PROVIDER:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.ADD_PROVIDER_ID;
                    break;
                case EFoodDetailType.TREATMENT:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.ADD_TREATMENT_ID;
                    break;
                case EFoodDetailType.PACKAGING:
                    foodDetail.TypeId = FoodDetailTypeDataConstant.ADD_PACKAGING_ID;
                    break;
                default: break;
            }
            await _foodDetailRepository.InsertAsync(foodDetail);
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

        public async Task UpdateFoodTreatment(ProviderFood food, int foodId, int treatmentId, int premisesId)
        {
            ProviderFood result = await _providerFoodRepository.FindAsync(x => x.FoodId == foodId & x.PremisesId == premisesId);
            result.TreatmentId = treatmentId;
            await _providerFoodRepository.UpdateAsync(result);
        }

        public async Task<ProviderFood> getFoodById(int foodId, int premisesId)
        {
            return await _providerFoodRepository.FindAsync(x => x.FoodId == foodId & x.PremisesId == premisesId);
        }

        public async Task<int> createDistributorFood(DistributorFood newDistributorFood)
        {
            return await _distributorFoodRepository.createDistributorFood(newDistributorFood);
        }

        public async Task<IList<DistributorFood>> getAllFoodByDistributorId(int distributorId)
        {
            var food = await _distributorFoodRepository.getAllFoodByDistributorId(distributorId);
            foreach (var i in food)
            {
                i.Food = _productRepos.GetById(i.FoodId);
                i.Food.Category = _categoryRepos.GetById(i.Food.CategoryId);
            }
            return food;
        }

        public Task<Food> getFoodById(int foodId)
        {
            return null;
        }

        public Task AddDetail(long foodId, EFoodDetailType type)
        {
            throw new System.NotImplementedException();
        }
    }
}
