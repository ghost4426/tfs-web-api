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
    public class DistributorController : ControllerBase
    {
        private readonly IFoodBL _productBL;
        private readonly IMapper _mapper;

        public DistributorController (IFoodBL productBL, IMapper mapper)
        {
            _mapper = mapper;
            _productBL = productBL;
        }
        [HttpGet("getProductMatched")]
        public async Task<IActionResult> getMatchedWithNumber()
        {
            //string disID = User.Claims.First(c => c.Type == "PremisesId").Value;
            var disID = 4; 
            return Ok(new { data = _mapper.Map<IList<Models.Food>>(await _productBL.getMatchedWithNumber(disID))});
        }

    }
}