using BusinessLogic.IBusinessLogic;
using Common.Constant;
using Common.Enum;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
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
        private readonly IFeedingFoodRepository _feedingFoodRepository;
        private readonly ITransactionRepository _transactionRepos;
        private readonly IPremisesRepository _premisesRepos;

        public FoodBLImpl(
            IFoodRepository productRepos
            , ICategoryRepository categoryRepos
            , IDistributorFoodRepository distributorFoodRepository
            , IProviderFoodRepository providerFoodRepository
            , IFoodDetailRepository foodDetailRepository
            , IFeedingFoodRepository feedingFoodRepository
            , ITransactionRepository transactionRepository
            , IPremisesRepository premisesRepository
            )
        {
            _productRepos = productRepos;
            _categoryRepos = categoryRepos;
            _distributorFoodRepository = distributorFoodRepository;
            _providerFoodRepository = providerFoodRepository;
            _foodDetailRepository = foodDetailRepository;
            _feedingFoodRepository = feedingFoodRepository;
            _transactionRepos = transactionRepository;
            _premisesRepos = premisesRepository;
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
            result.IsTreatmented = true;
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


        public Task InsertFeedingFood()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Food>> FarmReportFoodIn(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _productRepos.FindAllAsync(x => x.FarmId == premisesId && x.CreateDate.Month == month );
            foreach(var c in result)
            {
                c.Category = _categoryRepos.GetById(c.CategoryId);                
            }
            return result;
        }

        public async Task<IList<Transaction>> FarmReportFoodOut(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _transactionRepos.FindAllAsync(x => x.SenderId == premisesId && x.CreateDate.Month == month && x.StatusId == 3);
            foreach(var t in result)
            {                
                t.Food = _productRepos.GetById(t.FoodId);
                t.Food.Category = _categoryRepos.GetById(t.Food.CategoryId);
                t.Receiver = _premisesRepos.GetById(t.ReceiverId);
            }
            return result;
        }

        public async Task<IList<Transaction>> FarmReportFoodReject(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _transactionRepos.FindAllAsync(x => x.SenderId == premisesId && x.CreateDate.Month == month && x.StatusId == 4);
            foreach (var t in result)
            {
                t.Food = _productRepos.GetById(t.FoodId);
                t.Food.Category = _categoryRepos.GetById(t.Food.CategoryId);
                t.Receiver = _premisesRepos.GetById(t.ReceiverId);
            }
            return result;
        }

        public async Task<IList<ProviderFood>> ProviderReportFoodIn(int premisesId)
        {
            int month = DateTime.Now.Month;
            var result = await _providerFoodRepository.FindAllAsync(x => x.PremisesId == premisesId && x.CreateDate.Month == month);
            foreach (var t in result)
            {
                t.Food = _productRepos.GetById(t.FoodId);
                t.Food.Category = _categoryRepos.GetById(t.Food.CategoryId);
                t.Food.Farm = _premisesRepos.GetById(t.Food.FarmId);
            }
            return result;
        }

        public async Task UpdateFoodSoldOut(int foodId)
        {
            var food = _productRepos.GetById(foodId);
            food.IsSoldOut = true;
            await _productRepos.UpdateAsync(food);
        }

        public async Task UpdatePackagingFood(int foodId, int premisesId)
        {
            ProviderFood result = await _providerFoodRepository.FindAsync(x => x.FoodId == foodId & x.PremisesId == premisesId);
            result.IsPacked = true;
            await _providerFoodRepository.UpdateAsync(result);
        }
    }
}
