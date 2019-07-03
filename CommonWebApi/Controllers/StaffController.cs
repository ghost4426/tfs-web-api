using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using System.Linq.Expressions;
using DTO.Models.FoodData;
using Common.Utils;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IMaterialBL _materialBL;
        private IProductBL _productBL;
        private IFoodDataBL _foodDataBL;
        private IAutoMapConverter<Models.CreateFoodRequest, Entities.Food> _mapCreateFoodRequestModelToEntity;

        public StaffController(
            IMaterialBL materialBL,
            IProductBL productBL,
            IFoodDataBL foodDataBL,
            IAutoMapConverter<Models.CreateFoodRequest, Entities.Food> mapCreateFoodRequestModelToEntity)
        {
            _materialBL = materialBL;
            _productBL = productBL;
            _foodDataBL = foodDataBL;
            _mapCreateFoodRequestModelToEntity = mapCreateFoodRequestModelToEntity;
        }


        // GET api/values
        [HttpGet("getByProvider")]
        public async Task<IList<Entities.Food>> FindAllProductByProviderAsync(int providerID)
        {
            return await _productBL.FindAllProductByProviderAsync(providerID);
        }



        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Entities.Food>> getMatchedWithNumber(int distributorId)
        {
            return await _productBL.getMatchedWithNumber(distributorId);
        }

        //[HttpGet("getMaterialMatched/{FarmerId}")]
        //public async Task<IEnumerable<Entities.Material>> GetMaterialByFarmerId(int FarmerId)
        //{
        //    return await _materialBL.GetMaterialByFarmerId(FarmerId);
        //}
            
        //[HttpPost("createFood")]
        //public async Task<Models.ProductReponse.CreateProductReponse> CreateFood([FromBody]Models.CreateFoodRequest foodRequest )
        //{
        //    Entities.Food food = _mapCreateFoodRequestModelToEntity.ConvertObject(foodRequest);
        //        //new Entities.Food() { CategoriesId = foodRequest.CategoriesId, FarmerId = foodRequest.FamerId };
        //    await _productBL.CreateProductAsync(food);
        //    var reponseModel = new Models.ProductReponse.CreateProductReponse()
        //    {
        //        ProductId = food.FoodId
        //    };
        //    return reponseModel;
        //}

        [HttpGet("getFoodData")]
        public async Task<FoodData> GetFoodDataById(long id)
        {
            return await _foodDataBL.GetFoodDataByID(id);
        }

        [HttpPost("saveFoodData")]
        public async Task<string> SaveFoodData()
        {
            FoodData foodData = new FoodData()
            {
                Id = 1,
                Category = "Thịt Heo",
                Farm = new Farm()
                {
                    Name = "Farm Test",
                    FarmId = 1
                },
                Distributor = new Distributor()
                {
                    Name = "Dis test"
                },
                Provider = new Provider()
                {
                    Name = "Provider test"
                }
            };
            return await _foodDataBL.SaveFoodData(foodData);
        }

    }
}