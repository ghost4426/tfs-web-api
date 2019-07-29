using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Common.Utils;
using Common.Constant;
using AutoMapper;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Provider")]
    public class ProviderController : ControllerBase
    {
        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly ITransactionBL _transactionBL;
        private readonly ITreatmentBL _treatmentBL;
        private readonly IMapper _mapper;

        public ProviderController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            ITransactionBL transactionBL,
            ITreatmentBL treatmentBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _transactionBL = transactionBL;
            _treatmentBL = treatmentBL;
            _mapper = mapper;
        }

        [HttpPost("treatment/{foodId}")]
        public async Task<IActionResult> CreateTreatment(int foodId,[FromBody]Models.CreateTreatmentRequest treatmentRequest)
        {
            var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
            var TreatmentProcess = treatmentRequest.TreatmentProcess;
            var test = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.CreatedById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            Treatment.CreatedDate = DateTime.Now;
            Entities.Food food = await _foodBL.getFoodById(foodId);
            await _treatmentBL.CreateTreatment(Treatment, TreatmentProcess);
            food.TreatmentId = Treatment.TreatmentId;
            await _foodBL.UpdateFoodTreatment(food, foodId);
            return Ok(new { message = MessageConstant.INSERT_SUCCESS });
        }

        //More treatmentDetail
        [HttpPost("moreTreatment/{foodId}")]
        public async Task<IActionResult> CreateMoreTreatment(int foodId, [FromBody]Models.CreateMoreTreatmentRequest treatmentRequest)
        {
            var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
            var TreatmentProcess = treatmentRequest.TreatmentProcess;
            Treatment.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.CreatedById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            Treatment.CreatedDate = DateTime.Now;
            Entities.Food food = await _foodBL.getFoodById(foodId);
            int treatmentId = food.TreatmentId.GetValueOrDefault();
            await _treatmentBL.CreateMoreTreatmentDetail(treatmentId,Treatment, TreatmentProcess);
            return Ok(new { message = MessageConstant.INSERT_SUCCESS });
        }

        [HttpPut("food/treatment/{foodId}")]
        public async Task<string> AddTreatment(long foodId, [FromBody]string treatmentId)
        {
            await _foodBL.AddDetail(foodId, EFoodDetailType.TREATMENT);
            return await _foodDataBL.AddTreatment(foodId, int.Parse(treatmentId));
        }

        [HttpPut("food/packaging/{foodId}")]
        public async Task<string> Packaging(long foodId, [FromBody]Models.PackagingRequest packagingRequest)
        {
            await _foodBL.AddDetail(foodId, EFoodDetailType.PACKAGING);
            var Packaging = _mapper.Map<Models.FoodData.Packaging>(packagingRequest);
            return await _foodDataBL.Packaging(foodId, Packaging);
        }

        [HttpGet("getFoodByProvider")]
        public async Task<IActionResult> FindAllProviderFoodAsync()
        {            
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.FoodProvider>>(await _foodBL.getAllFoodByProviderId(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("countProviderTransaction")]
        public async Task<int> CountTransaction()
        {
            int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            return await _transactionBL.CountProviderTransaction(premisesId);
        }

        [HttpGet("getAllProviderTransaction")]
        public async Task<IActionResult> getAllTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.ProviderGetTransaction>>(await _transactionBL.getAllProviderTransaction(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpPut("UpdateTransaction/{transactionId}")]
        public async Task<string> UpdateTransaction(int transactionId, [FromBody] Models.TransactionUpdateRequest trans)
        {
            try
            {
                Entities.Transaction transaction = new Entities.Transaction()
                {
                    TransactionId = transactionId,
                    StatusId = trans.StatusId,
                    RejectedReason = trans.RejectedReason,
                    ProviderComment = trans.ProviderComment,
                };
                await _transactionBL.UpdateTransaction(transaction, transactionId);
                return "OK";
            }catch(Exception e)
            {
                return e.ToString();
            }
        }

        [HttpPost("providerFood")]
        public async Task<int> CreateProviderFood([FromBody]Models.CreateProviderFoodRequest foodRequest)
        {
            Entities.ProviderFood food = _mapper.Map<Entities.ProviderFood>(foodRequest);
            food.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            return await _foodBL.createProviderFood(food);
        }

        [HttpGet("foodTreatment/{foodId}")]
        public async Task<IActionResult> getAllTreatmentById(int foodId)
        {
            try
            {
                Entities.Food food = await _foodBL.getFoodById(foodId);
                int treatmentId = food.TreatmentId.GetValueOrDefault();
                return Ok(new { data = _mapper.Map<IList<Models.FoodRespone.TreatmentReponse>>(await _treatmentBL.getAllTreatmentById(treatmentId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }
    }
}