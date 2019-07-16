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

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IFoodBL _foodBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly ITreatmentBL _treatmentBL;
        private readonly IMapper _mapper;

        public ProviderController(
            IFoodBL foodBL,
            IFoodDataBL foodDataBL,
            ITreatmentBL treatmentBL,
            IMapper mapper)
        {
            _foodBL = foodBL;
            _foodDataBL = foodDataBL;
            _treatmentBL = treatmentBL;
            _mapper = mapper;
        }

        [HttpPost("treatment")]
        public async Task<IActionResult> CreateTreatment([FromBody]Models.CreateTreatmentRequest treatmentRequest)
        {
            var Treatment = _mapper.Map<Entities.Treatment>(treatmentRequest);
            var TreatmentProcess = treatmentRequest.TreatmentProcess;
            Treatment.PremisesId = 2;
            await _treatmentBL.CreateTreatment(Treatment, TreatmentProcess);
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

        //[HttpGet("testgetByProvider")]
        //public async Task<IList<Models.FoodProvider>> TestFindAllProductByProviderAsync()
        //{
        //    IList<Entities.Food> list = await _foodBL.FindAllProductByProviderAsync(2);
        //    var result = _mapper.Map <IList<Models.FoodProvider>>(list);
        //    return result;
        //}

        //[HttpGet("getByProvider")]
        //public async Task<IList<Entities.Food>> FindAllProductByProviderAsync()
        //{
        //    int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);

        //    return await _foodBL.FindAllProductByProviderAsync(userId);
        //}

        [HttpGet("getFoodByProvider")]
        public async Task<IActionResult> FindAllProviderFoodAsync()
        {            
            try
            {
                //int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.FoodProvider>>(await _foodBL.getAllFoodByProviderId(2)) });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }
    }
}