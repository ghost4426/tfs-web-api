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
        private readonly IPremisesBL _premisesBL;
        private readonly IMapper _mapper;

        public ProviderController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            ITransactionBL transactionBL,
            ITreatmentBL treatmentBL,
            IPremisesBL premisesBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _transactionBL = transactionBL;
            _treatmentBL = treatmentBL;
            _premisesBL = premisesBL;
            _mapper = mapper;
        }

        [HttpPost("treatment")]
        public async Task<IActionResult> CreateTreatment([FromBody]Models.CreateTreatmentRequest treatmentRequest)
        {
            var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
            var TreatmentProcess = treatmentRequest.TreatmentProcess;
            var test = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            Treatment.CreateDate = DateTime.Now;
            await _treatmentBL.CreateTreatment(Treatment, TreatmentProcess);
            return Ok(new { message = MessageConstant.INSERT_SUCCESS });
        }

        //More treatmentDetail
        [HttpPost("moreTreatment/{treatmentId}")]
        public async Task<IActionResult> CreateMoreTreatment(int treatmentId, [FromBody]Models.CreateMoreTreatmentRequest treatmentRequest)
        {
            var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
            var TreatmentProcess = treatmentRequest.TreatmentProcess;
            IList<int> treatment = await _treatmentBL.getTreatmentIdByParent(treatmentId);
            foreach (var id in treatment)
            {
                await _treatmentBL.deleteTreatment(id);
            }
            Treatment.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            Treatment.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            Treatment.CreateDate = DateTime.Now;
            await _treatmentBL.CreateMoreTreatmentDetail(treatmentId, Treatment, TreatmentProcess);
            return Ok(new { message = MessageConstant.INSERT_SUCCESS });
        }

        [HttpPut("food/treatment/{foodId}")]
        public async Task<IActionResult> AddTreatment(long foodId, [FromBody]string treatmentId)
        {

            try
            {
                int providerId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                await _foodBL.AddDetail(foodId, EFoodDetailType.TREATMENT);
                Entities.ProviderFood food = await _foodBL.getFoodById((int)foodId, providerId);
                await _foodBL.UpdateFoodTreatment(food, (int)foodId, int.Parse(treatmentId),providerId);
                return Ok(new { message = await _foodDataBL.AddTreatment(foodId, int.Parse(treatmentId), providerId) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Error = ex.ToString() });
            }

        }

        [HttpPut("food/packaging/{foodId}")]
        public async Task<string> Packaging(long foodId, [FromBody]Models.PackagingRequest packagingRequest)
        {
            int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            await _foodBL.AddDetail(foodId, EFoodDetailType.PACKAGING);
            var Packaging = _mapper.Map<Models.FoodData.Packaging>(packagingRequest);
            return await _foodDataBL.Packaging(foodId, Packaging, premisesId);
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

        [HttpGet("getAllProviderReceiveTransaction")]
        public async Task<IActionResult> getAllReceiveTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.ProviderGetTransaction>>(await _transactionBL.getAllProviderReceiveTransaction(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("getAllProviderSendTransaction")]
        public async Task<IActionResult> getAllSendTransaction()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.ProviderGetSendTransaction>>(await _transactionBL.getAllProviderSendTransaction(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpPut("UpdateTransaction/{transactionId}")]
        public IActionResult UpdateTransaction(int transactionId, [FromBody] Models.TransactionUpdateRequest trans)
        {
            try
            {
                Entities.Transaction transaction = new Entities.Transaction()
                {
                    TransactionId = transactionId,
                    StatusId = trans.StatusId,
                    RejectReason = trans.RejectedReason,
                    ReceiverComment = trans.ProviderComment,
                };                
                _transactionBL.UpdateTransaction(transaction, transactionId);
                return Ok(new { message = MessageConstant.UPDATE_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpPost("providerFood")]
        public async Task<IActionResult> CreateProviderFood([FromBody]Models.CreateProviderFoodRequest foodRequest)
        {
            try
            {
                Entities.ProviderFood food = _mapper.Map<Entities.ProviderFood>(foodRequest);
                food.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                await _foodDataBL.AddProvider(food.FoodId, int.Parse(User.Claims.First(c => c.Type == "premisesID").Value));
                await _foodBL.createProviderFood(food);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
            
        }

        [HttpGet("foodTreatment/{treatmentId}")]
        public async Task<IActionResult> getAllTreatmentById(int treatmentId)
        {
            try
            {
                return Ok(new { data = _mapper.Map<IList<Models.FoodRespone.TreatmentReponse>>(await _treatmentBL.getAllTreatmentById(treatmentId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("treatment")]
        public async Task<IActionResult> getAllTreatmentByPremisesId()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _treatmentBL.getAllTreatmentByPremisesId(premisesId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpDelete("deleteTreatment/{treatmentId}")]
        public async Task<IActionResult> deleteTreatment(int treatmentId)
        {
            try
            {
                await _treatmentBL.deleteTreatment(treatmentId);
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("getAllDistributor")]
        public async Task<IActionResult> getAllDistributorAsync(string search)
        {
            try
            {
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _premisesBL.getAllDistriburtorAsync(search)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("getFoodDataByProvider")]
        public async Task<IActionResult> GetFoodDataByIDAndProviderID(long id)
        {
            try
            {
                int providerId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = await _foodDataBL.GetFoodDataByIDAndProviderID(id, providerId) });
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
            transaction.SenderId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
            transaction.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
            await _transactionBL.CreateSellFoodTransactionAsync(transaction);
            var reponseModel = new Models.TransactionReponse.CreateTransactionReponse()
            {
                TransactionId = transaction.TransactionId
            };
            return reponseModel;
        }
    }
}