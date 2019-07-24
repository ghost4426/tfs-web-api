using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.IBusinessLogic;
using Common.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {

        private readonly IFoodDetailBL _foodDetailBL;
        private readonly IMapper _mapper;
        public CommonController(
            IFoodDetailBL foodDetailBL,
            IMapper mapper)
        {
            _foodDetailBL = foodDetailBL;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("productdetailtype")]
        public async Task<IActionResult> GetProductDetailType()
        {
            try
            {
                return Ok(new{ results = _mapper.Map<IList<Models.Option>>(await _foodDetailBL.GetFoodDetailTypeByPremises(PremisesTypeDataConstant.FARM))});
            }
            catch (Exception e)
            {
                return BadRequest(new {msg = e.Message});
            }
        }
    }
}