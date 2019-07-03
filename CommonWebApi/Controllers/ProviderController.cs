﻿using System;
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

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private IFoodBL _foodBL;
        private IFoodDataBL _foodDataBL;
        private ITreatmentBL _treatmentBL;
        private IMapper _mapper;

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
            return await _foodDataBL.AddTreatment(foodId, int.Parse(treatmentId));
        }

        [HttpPut("food/packaging/{foodId}")]
        public async Task<string> Packaging(long foodId, [FromBody]Models.PackagingRequest packagingRequest)
        {
            var Packaging = _mapper.Map<Models.FoodData.Packaging>(packagingRequest);
            return await _foodDataBL.Packaging(foodId, Packaging);
        }

        [HttpGet("testgetByProvider")]
        public async Task<IList<Food>> TestFindAllProductByProviderAsync()
        {
            return await _productBL.FindAllProductByProviderAsync(3);
        }

        [HttpGet("getByProvider")]
        public async Task<IList<Food>> FindAllProductByProviderAsync()
        {
            int userId = Int32.Parse(User.Claims.First(c => c.Type == "UserID").Value);

            return await _productBL.FindAllProductByProviderAsync(userId);
        }
    }
}