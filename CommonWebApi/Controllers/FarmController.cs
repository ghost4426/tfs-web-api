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
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;

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
        private readonly IFeedingBL _feedingBL;
        private readonly IVaccineBL _vaccineBL;
        private readonly IMapper _mapper;
        public FarmController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            IPremisesBL premisesBL,
            ITransactionBL transactionBL,
            IFoodDetailBL foodDetailBL,
            IFeedingBL feedingBL,
            IVaccineBL vaccineBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _premisesBL = premisesBL;
            _transactionBL = transactionBL;
            _foodDetailBL = foodDetailBL;
            _feedingBL = feedingBL;
            _vaccineBL = vaccineBL;
            _mapper = mapper;
        }

        [HttpGet("foods")]
        public async Task<IActionResult> GetAllFood()
        {
            try
            {
                var farmId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.FoodFarm>>(await _foodBL.FindAllProductByFarmerAsync(farmId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }

        [HttpPost("food")]
        public async Task<IActionResult> CreateFood([FromBody]Models.CreateFoodRequest foodRequest)
        {
            try
            {
                Entities.Food food = _mapper.Map<Entities.Food>(foodRequest);
                food.FarmId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                food.CreateById = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _foodBL.CreateProductAsync(food);
                var transactionHash = await _foodDataBL.CreateFood(food, food.FarmId);
                await _foodBL.AddDetail(food.FoodId, EFoodDetailType.CREATE, transactionHash, food.CreateById);
                return Ok(new { messeage = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }

        [HttpGet("food/feedings/{foodId}")]
        public async Task<IActionResult> GetFeedingsById(int foodId)
        {
            try
            {
                return Ok(new { data = await _foodDataBL.GetFeedingsById(foodId) });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPut("food/feedings/{foodId}")]
        public async Task<IActionResult> AddFeedings(int foodId, [FromBody]List<string> feedings)
        {
            
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var transactionHash = await _foodDataBL.AddFeedings(foodId, feedings);
                await _foodBL.AddDetail(foodId, EFoodDetailType.FEEDING, transactionHash, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }

        }


        [HttpGet("food/vaccinations/{foodId}")]
        public async Task<IActionResult> GetVaccinsById(int foodId)
        {
            try
            {
                return Ok(new { data = await _foodDataBL.GetVaccinsById(foodId) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPut("food/vaccinations/{foodId}")]
        public async Task<IActionResult> AddVaccination(int foodId, [FromBody]List<Models.AddVaccineInfoToFoodDataRequest> vaccineModelRequest)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var transactionHash = await _foodDataBL.AddVaccination(foodId, vaccineModelRequest);
                await _foodBL.AddDetail(foodId, EFoodDetailType.VACCINATION, transactionHash, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
           
        }

        //[HttpPut("food/verify/{foodId}")]
        //public async Task<string> Addverify(int foodId, [FromBody]string certificationNumber)
        //{
        //    var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
        //    var transactionHash= await _foodDataBL.AddCertification(foodId, certificationNumber);
        //    await _foodBL.AddDetail(foodId, EFoodDetailType.VERIFY, transactionHash, userId);
        //    return 
        //}

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
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
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

        [HttpGet("getAllProvider")]
        public async Task<IActionResult> GetAllProvider(string search)
        {
            try
            {
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(await _premisesBL.getAllProviderAsync(search)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
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

        [HttpPost("feedings")]
        public IActionResult AddFeedingList([FromBody]List<Models.Feedingm> feedings)
        {
            try
            {
                var feedingList = _mapper.Map<IList<Entities.Feeding>>(feedings);


                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                _feedingBL.AddNewFeedingList(feedingList, premisesId, userId);
                return Ok(new { msg = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("premisesFeedings")]
        public async Task<IActionResult> GetFeedingListByPremisesId()
        {
            try
            {
                var feedingList = await _feedingBL.GetFeedingListByPremisesId(int.Parse(User.Claims.First(c => c.Type == "premisesID").Value));
                return Ok(new { data = _mapper.Map<IList<Models.Feedingm>>(feedingList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("feedings")]
        public async Task<IActionResult> GetFeedingList()
        {
            try
            {
                var feedingList = await _feedingBL.GetFeedingList();
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(feedingList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpPut("feeding/{id}")]
        public async Task<IActionResult> RemoveFeeding(int id)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _feedingBL.RemoveFeedingById(id, userId);
                return Ok(new { msg = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message, error = e.StackTrace });
            }
        }

        [HttpPost("vaccines")]
        public IActionResult AddVaccineList([FromBody]List<Models.Vaccinem> vaccines)
        {
            try
            {
                var vaccineList = _mapper.Map<IList<Entities.Vaccine>>(vaccines);
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                var premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                _vaccineBL.AddNewVaccineList(vaccineList, premisesId, userId);
                return Ok(new { message = MessageConstant.INSERT_SUCCESS });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("premisesVaccines")]
        public async Task<IActionResult> GetVaccineListByPremisesId()
        {
            try
            {
                var vaccineList = await _vaccineBL.GetVaccineListByPremisesId(int.Parse(User.Claims.First(c => c.Type == "premisesID").Value));
                return Ok(new { data = _mapper.Map<IList<Models.Vaccinem>>(vaccineList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpGet("vaccines")]
        public async Task<IActionResult> GetFVaccineList()
        {
            try
            {
                var vaccineList = await _vaccineBL.GetVaccineList();
                return Ok(new { results = _mapper.Map<IList<Models.Option>>(vaccineList) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        [HttpDelete("vaccine/{id}")]
        public async Task<IActionResult> RemoveVaccine(int id)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _vaccineBL.RemoveVaccineById(id, userId);
                return Ok(new { msg = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message, error = e.StackTrace });
            }
        }
    }
}
