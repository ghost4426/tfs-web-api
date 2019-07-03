using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Common.Utils;
using AutoMapper;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerController : ControllerBase
    {

        private IFoodBL _foodBL;
        private IFoodDataBL _foodDataBL;
        private IMapper _mapper;
        public FarmerController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _mapper = mapper;
        }

        [HttpPost("food")]
        public async Task<string> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            Entities.Food food = _mapper.Map<Entities.Food>(foodRequest);
            food.FoodId = 99;
            return await _foodDataBL.CreateFood(food, 2);
        }

        [HttpPut("food/feedings/{foodId}")]
        public async Task<string> AddFeedings(long foodId, [FromBody]List<string> feedings)
        {
            return await _foodDataBL.AddFeedings(foodId, feedings);
        }

        [HttpPut("food/vaccination/{foodId}")]
        public async Task<string> AddVaccination(long foodId, [FromBody]string vaccinationType)
        {
            return await _foodDataBL.AddVaccination(foodId, vaccinationType);
        }

        [HttpPut("food/certification/{foodId}")]
        public async Task<string> AddCertification(long foodId, [FromBody]string certificationNumber)
        {
            return await _foodDataBL.AddCertification(foodId, certificationNumber);
        }

        [HttpGet("getAllCategory")]
        public async Task<IList<Categories>> getAllCategory()
        {
            return await _productBL.getAllCategory();
        }

        [HttpGet("getByFarmer")]
        public async Task<IList<Food>> FindAllProductByFarmerAsync()
        {
            return await _productBL.FindAllProductByFarmerAsync(2);
        }

        [HttpPost("createFood")]
        public async Task<Models.ProductReponse.CreateProductReponse> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            Entities.Food food = _mapCreateFoodRequestModelToEntity.ConvertObject(foodRequest);
            //new Entities.Food() { CategoriesId = foodRequest.CategoriesId, FarmerId = foodRequest.FamerId };
            await _productBL.CreateProductAsync(food);
            var reponseModel = new Models.ProductReponse.CreateProductReponse()
            {
                ProductId = food.FoodId
            };
            return reponseModel;
        }
    }
}