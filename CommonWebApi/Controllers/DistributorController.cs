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
        private readonly IMapper     _mapper;
        private readonly ITransactionBL _transactionBL;

        public DistributorController (IFoodBL productBL, IMapper mapper, ITransactionBL transactionBL)
        {
            _mapper = mapper;
            _productBL = productBL;
            _transactionBL = transactionBL;
        }
        [HttpGet("getProductMatched")]
        public async Task<IActionResult> getMatchedWithNumber()
        {
            //string disID = User.Claims.First(c => c.Type == "PremisesId").Value;
            var disID = 4; 
            return Ok(new { data = _mapper.Map<IList<Models.Food>>(await _productBL.getMatchedWithNumber(disID))});
        }

        [HttpGet("getTransactionById")]
        public async Task<IActionResult> getTransactionById()
        {
            var TransID = 3;
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.GetTransactionById(TransID)) });
        }

        [HttpPut("updateTransaction")]
        public async Task<IActionResult> UpdateTransactionStatus()
        {
            var TransID = 3;
            var status = 3;
            var reason = "NO REASON";
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.UpdateTransaction(TransID, status, reason)) });
        }
    }
}