using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using DTO.Models.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Common.Utils;
using DTO.Models.Exception;
using Common.Constant;
using Newtonsoft.Json;
using DTO.Models.FoodData;
using ContractInteraction.ContractServices;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFoodDataBL _foodDataBL;
        private readonly IContractServices _contractServices;
        private readonly JWTSetttings _appSettings;

        public TestController(
            IFoodDataBL foodDataBL,
            IContractServices contractServices)
        {
            _foodDataBL = foodDataBL;
            _contractServices = contractServices;
        }

        [HttpPost("AddFood")]
        public async Task<IActionResult> AddFood(int foodId, string category, string breed)
        {
            try
            {
                Models.FoodData.FoodData foodData = new FoodData()
                {
                    FoodId = foodId,
                    Category = category,
                    Breed = breed
                };
                return Ok( new { result = await _foodDataBL.AddFoodData(foodData) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        [HttpPost("Transaction")]
        public async Task<IActionResult> GetTransaction(string hash)
        {
            try
            {
                return Ok(new { result = await _contractServices.GetTransactionByHashAsync(hash) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
            }
        }

        //[HttpPost("Decode")]
        //public async Task<IActionResult> DeGetTransaction(string hash)
        //{
        //    try
        //    {
        //        return Ok(new { result = await _contracctServices.GetTransactionByHashAsync(hash) });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = ex.StackTrace });
        //    }
        //}


    }
}