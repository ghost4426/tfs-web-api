﻿using BusinessLogic.IBusinessLogic;
using Common.Enum;
using DataAccess.IRepositories;
using DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class FoodBLImpl : IFoodBL
    {
        private IProductRepository _productRepos;
        private ICategoryRepository _categoryRepos;

        public FoodBLImpl(IProductRepository productRepos, ICategoryRepository categoryRepos)
        {
            _productRepos = productRepos;
            _categoryRepos = categoryRepos;
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
                var cat = _categoryRepos.GetById(product.CategoriesId);
                product.Categories = cat;
            }
            return products;
        }

        public async Task<int> CreateProductAsync(Food newProduct)
        {
            return await this._productRepos.CreateProductAsync(newProduct);
        }

        public async Task<IEnumerable<Food>> getMatchedWithNumber(int distributorId)
        {
            return await this._productRepos.GetMatchedWithNumber(distributorId);
        }

        public async Task<IList<Categories>> getAllCategory()
        {
            return await this._categoryRepos.GetAllAsync();
        }

        public async Task<IList<Food>> FindAllProductByFarmerAsync(int farmerID)
        {
            var products = await this._productRepos.FindAllProductByFarmerAsync(farmerID);
            foreach (var product in products)
            {
                var cat = _categoryRepos.GetById(product.CategoriesId);
                product.Categories = cat;
            }
            return products;
        }

        public async Task AddDetail(long foodId, EFoodDetailType type)
        {
            var food =  _productRepos.GetById((int)foodId);
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
    }
}
