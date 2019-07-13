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
using Common.Enum;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {

        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly IPremisesBL _premisesBL;
        private readonly ITransactionBL _transactionBL;
        private readonly IMapper _mapper;
        public FarmController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            IPremisesBL premisesBL,
            ITransactionBL transactionBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _premisesBL = premisesBL;
            _transactionBL = transactionBL;
            _mapper = mapper;
        }

        [HttpPost("food")]
        public async Task<string> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            Entities.Food food = _mapper.Map<Entities.Food>(foodRequest);
            food.FarmId = 1;
            await _foodBL.CreateProductAsync(food);
            return await _foodDataBL.CreateFood(food, food.FarmId);
        }

        [HttpPut("food/feedings/{foodId}")]
        public async Task<string> AddFeedings(long foodId, [FromBody]List<string> feedings)
        {
            await _foodBL.AddDetail(foodId, EFoodDetailType.FEEDING);
            return await _foodDataBL.AddFeedings(foodId, feedings);
        }

        [HttpPut("food/vaccination/{foodId}")]
        public async Task<string> AddVaccination(long foodId, [FromBody]string vaccinationType)
        {
            await _foodBL.AddDetail(foodId, EFoodDetailType.VACCINATION);
            return await _foodDataBL.AddVaccination(foodId, vaccinationType);
        }

        [HttpPut("food/certification/{foodId}")]
        public async Task<string> AddCertification(long foodId, [FromBody]string certificationNumber)
        {
            await _foodBL.AddDetail(foodId, EFoodDetailType.CERTIFICATION);
            return await _foodDataBL.AddCertification(foodId, certificationNumber);
        }

        [HttpGet("getAllCategory")]
        public async Task<IList<Entities.Category>> getAllCategory()
        {
            return await _foodBL.getAllCategory();
        }

        //[HttpGet("getByFarmer")]
        //public async Task<IList<Models.FoodFarm>> FindAllProductByFarmerAsync()
        //{
        //    IList<Entities.Food> list = await _foodBL.FindAllProductByFarmerAsync(1);
        //    var result = _mapper.Map<IList<Models.FoodFarm>>(list); 
        //    return result;        
        //}

        [HttpGet("getByFarmer")]
        public async Task<IActionResult> FindAllProductByFarmerAsync()
        {
            return Ok(new { data = _mapper.Map<IList<Models.FoodFarm>>(await _foodBL.FindAllProductByFarmerAsync(1)) });
        }

        //[HttpPost("createFood")]
        //public async Task<Models.ProductReponse.CreateProductReponse> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        //{
        //    Entities.Food food = _mapCreateFoodRequestModelToEntity.ConvertObject(foodRequest);
        //    //new Entities.Food() { CategoriesId = foodRequest.CategoriesId, FarmerId = foodRequest.FamerId };
        //    await _foodBL.CreateProductAsync(food);
        //    var reponseModel = new Models.ProductReponse.CreateProductReponse()
        //    {
        //        ProductId = food.FoodId
        //    };
        //    return reponseModel;
        //}

        [HttpPost("createTransaction")]
        public async Task<Models.TransactionReponse.CreateTransactionReponse> CreateTransaction([FromBody]Models.TransactionRequest transactionRequest)
        {
            Entities.Transaction transaction = _mapper.Map<Entities.Transaction>(transactionRequest);
            await _transactionBL.CreateSellFoodTransactionAsync(transaction);
            var reponseModel = new Models.TransactionReponse.CreateTransactionReponse()
            {
                TransactionId = transaction.TransactionId
            };
            return reponseModel;
        }

        [HttpGet("getAllProvider")]
        public async Task<IActionResult> GetAllProvider(string keyword)
        {
            try
            {
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _premisesBL.getAllProviderAsync(keyword)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("food/foodDetail/{foodId}")]
        public async Task<Models.FoodData.FoodData> getFoodDetail(long foodId)
        {
            return await _foodDataBL.GetFoodDataByID(foodId);
        }
    }
}