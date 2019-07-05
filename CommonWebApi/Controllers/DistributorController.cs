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
    public class DistributorController : ControllerBase
    {
        private IFoodBL _productBL;
        public DistributorController (IFoodBL productBL)
        {
            _productBL = productBL;
        }
        [HttpGet("getProductMatched/{distributorId}")]
        public async Task<IEnumerable<Entities.Food>> getMatchedWithNumber(int distributorId)
        {
            return await _productBL.getMatchedWithNumber(distributorId);
        }

    }
}