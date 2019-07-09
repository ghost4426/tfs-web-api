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
        private IFoodDataBL _foodDataBL;

        public StaffController(
            IFoodDataBL foodDataBL)
        {
            _foodDataBL = foodDataBL;
        }

        [HttpGet("getFoodData")]
        public async Task<FoodData> GetFoodDataById(long id)
        {
            return await _foodDataBL.GetFoodDataByID(id);
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