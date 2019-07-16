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
using AutoMapper;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IFoodDataBL _foodDataBL;
        private readonly ITransactionBL _transactionBL;
        private readonly IMapper _mapper;

        public StaffController(
            IFoodDataBL foodDataBL,
            ITransactionBL transactionBL,
            IMapper mapper)
        {
            _foodDataBL = foodDataBL;
            _transactionBL = transactionBL;
            _mapper = mapper;
        }

        [HttpGet("getFoodData")]
        public async Task<FoodData> GetFoodDataById(long id)
        {
            return await _foodDataBL.GetFoodDataByID(id);
        }

        [HttpGet("countTransaction")]
        public async Task<int> CountTransaction()
        {
            //int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);
            int userId = 2;
            return await _transactionBL.CountTransaction(userId);
        }

        [HttpGet("getAllTransaction")]
        public async Task<IActionResult> getAllTransaction()
        {
            try
            {
                //int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);
                int userId = 2;
                return Ok(new { data = _mapper.Map<IList<Models.TransactionReponse.GetTransaction>>(await _transactionBL.getAllTransaction(userId)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }

        //[HttpPost("saveFoodData")]
        //public async Task<string> SaveFoodData()
        //{
        //    FoodData foodData = new FoodData()
        //    {
        //        FoodId = 1,
        //        Category = "Thịt Heo",
        //        Farm = new Farm()
        //        {
        //            Name = "Farm Test",
        //            FarmId = 1
        //        },
        //        Distributor = new Distributor()
        //        {
        //            Name = "Dis test"
        //        },
        //        Provider = new Provider()
        //        {
        //            Name = "Provider test"
        //        }
        //    };
        //    return await _foodDataBL.SaveFoodData(foodData);
        //}

    }
}