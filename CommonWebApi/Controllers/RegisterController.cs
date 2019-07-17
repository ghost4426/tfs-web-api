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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRegisterInfoBL _regBl;
        // GET: /<controller>/
        public RegisterController(
            IMapper mapper, 
            IRegisterInfoBL regBl
            )
        {
            _regBl = regBl;
            _mapper = mapper;
        }
        [HttpPost("premises")]
        public async Task<IActionResult> CreatePremises([FromBody]Models.CreateRegisterInfoRequest regInfo)
        {
            var isCreated = false;
            try
            {
                var newReg = _mapper.Map<Entities.RegisterInfo>(regInfo);
                await _regBl.CreateRegisterInfo(newReg);
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });
            }
            catch (DuplicatedUsernameException e)
            {
                return BadRequest(new { message = MessageConstant.DUPLICATED_USERNAME });
            }
            catch (DuplicatedPremisesNameException e)
            {
                return BadRequest(new { message = MessageConstant.DUPLICATED_PREMISESNAME });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
            }
        }
    }
}
