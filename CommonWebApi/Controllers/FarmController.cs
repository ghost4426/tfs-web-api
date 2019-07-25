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
using Microsoft.AspNetCore.Authorization;
using Common.Constant;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Farm")]
    public class FarmController : ControllerBase
    {

        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly IPremisesBL _premisesBL;
        private readonly ITransactionBL _transactionBL;
        private readonly IFoodDetailBL _foodDetailBL;
        private readonly IMapper _mapper;
        public FarmController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            IPremisesBL premisesBL,
            ITransactionBL transactionBL,
            IFoodDetailBL foodDetailBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _premisesBL = premisesBL;
            _transactionBL = transactionBL;
            _foodDetailBL = foodDetailBL;
            _mapper = mapper;
        }

        [HttpGet("foods")]
        public async Task<IActionResult> GetAllProduct()
        {
            var farmId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            return Ok(new { data = _mapper.Map<IList<Models.FoodFarm>>(await _foodBL.FindAllProductByFarmerAsync(farmId)) });
        }

        [HttpPost("food")]
        public async Task<string> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            Entities.Food food = _mapper.Map<Entities.Food>(foodRequest);
            food.FarmId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value); 
            food.CreatedById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            await _foodBL.CreateProductAsync(food);
            return await _foodDataBL.CreateFood(food, food.FarmId);
        }

        [HttpGet("food/feedings/{foodId}")]
        public async Task<IList<string>> GetFeedingsById(int foodId)
        {
            return await _foodDataBL.GetFeedingsByIdAsync(foodId);
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

        [HttpPut("food/verify/{foodId}")]
        public async Task<string> Addverify(long foodId, [FromBody]string certificationNumber)
        {
            await _foodBL.AddDetail(foodId, EFoodDetailType.VERIFY);
            return await _foodDataBL.AddCertification(foodId, certificationNumber);
        }

        [HttpGet("category")]
        public async Task<IList<Entities.Category>> GetAllCategory()
        {
            return await _foodBL.getAllCategory();
        }

        [HttpGet("productdetailtype")]
        public async Task<IActionResult> GetProductDetailType()
        {
            try
            {
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _foodDetailBL.GetFoodDetailTypeByPremises(PremisesTypeDataConstant.FARM)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }


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
        public async Task<IActionResult> GetAllProvider(string search)
        {
            try
            {
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _premisesBL.getAllProviderAsync(search)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("food/foodDetail/{foodId}")]
        public async Task<Models.FoodData.FoodData> GetFoodDetail(long foodId)
        {
            return await _foodDataBL.GetFoodDataByID(foodId);
        }

        [HttpGet("countFarmTransaction")]
        public async Task<int> CountTransaction()
        {
            int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            return await _transactionBL.CountFarmTransaction(premisesId);
        }

        [HttpGet("getAllFarmTransaction")]
        public async Task<IActionResult> getAllTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.FarmGetTransaction>>(await _transactionBL.getAllFarmTransaction(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }
    }
}