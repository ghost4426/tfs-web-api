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
    public class FarmerController : ControllerBase
    {

        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly IMapper _mapper;
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

        [HttpGet("getByFarmer")]
        public async Task<IList<Entities.Food>> FindAllProductByFarmerAsync()
        {
            return await _foodBL.FindAllProductByFarmerAsync(2);
        }

    }
}