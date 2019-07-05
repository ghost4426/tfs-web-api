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
        private IFoodBL _productBL;
        private IFoodDataBL _foodDataBL;

        public StaffController(
            IMaterialBL materialBL,
            IFoodBL productBL,
            IFoodDataBL foodDataBL)
        {
            _materialBL = materialBL;
            _productBL = productBL;
            _foodDataBL = foodDataBL;
        }


        // GET api/values
        [HttpGet("getByProvider")]
        public async Task<IList<Entities.Food>> FindAllProductByProviderAsync(int providerID)
        {
            return await _productBL.FindAllProductByProviderAsync(providerID);
        }

        //[HttpGet("getMaterialMatched/{FarmerId}")]
        //public async Task<IEnumerable<Entities.Material>> GetMaterialByFarmerId(int FarmerId)
        //{
        //    return await _materialBL.GetMaterialByFarmerId(FarmerId);
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
                FoodId = 1,
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