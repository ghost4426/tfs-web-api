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
using Microsoft.AspNetCore.Authorization;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinaryController : ControllerBase
    {
        private readonly IFoodBL _foodBL;
        private readonly IMapper _mapper;
        private readonly ITransactionBL _transactionBL;
        private readonly IUserBL _userBL;

        public VeterinaryController(IFoodBL productBL, IMapper mapper, ITransactionBL transactionBL, IUserBL userBL)
        {
            _mapper = mapper;
            _foodBL = productBL;
            _transactionBL = transactionBL;
            _userBL = userBL;
        }

        [HttpPut("transaction/{transID}")]
        public async Task<IActionResult> UpdateTransactionStatus(int tranId, [FromBody] Models.TransactionVerterinaryUpdateRequest trans)
        {
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.UpdateVerterinaryTransaction(tranId, trans.StatusId, trans.RejectedReason, trans.RejectById)) });
        }

        [HttpGet("transaction/{id}")]
        public async Task<IActionResult> getTransactionById(int id)
        {
            //var TransID = 3;
            return Ok(new { data = _mapper.Map<Models.Transaction>(await _transactionBL.GetTransactionById(id)) });
        }
    }
}