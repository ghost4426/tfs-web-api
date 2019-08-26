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

        [HttpGet("getFoodDataByProvider")]
        public async Task<FoodData> GetFoodDataByIDAndProviderID(long id, int providerId)
        {
            return await _foodDataBL.GetFoodDataByIDAndProviderID(id, providerId);
        }

        [HttpGet("getFoodDataByProviderAndDistributor")]
        public async Task<FoodData> GetFoodDataByIDAndProviderIDAndDistibutorID(long id, int providerId,int distributorId)
        {
            return await _foodDataBL.GetFoodDataByIDAndProviderIDAndDistributorID(id, providerId,distributorId);
        }
    }
}