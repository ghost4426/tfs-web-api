using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models = DTO.Models;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Constant;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly IMapper _mapper;

        public ManagerController(
            IUserBL userBL,
            IMapper mapper)
        {
            _userBL = userBL;
            _mapper = mapper;
        }

        [HttpGet("getUserByPremises")]
        public async Task<IActionResult> getUsersByPremises()
        {
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                return Ok(new { data = _mapper.Map<IList<Models.User>>(await _userBL.getUsersByPremises(premisesId)) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Error = ex.ToString() });
            }
        }

        [HttpPut("changeUserStatus/{userId}")]
        public async Task<IActionResult> changeUserStatus(int userId)
        {
            try
            {
                await _userBL.updateUserStatus(userId);
                return Ok(new { message = MessageConstant.UPDATE_SUCCESS });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, Error = ex.ToString() });
            }
        }
    }
}