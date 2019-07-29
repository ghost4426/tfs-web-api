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

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {

        private readonly IUserBL _userBL;
        private readonly IMapper _mapper;
        private readonly IEmailSender _mailSender;
        private readonly JWTSetttings _appSettings;


        public GuestController(
            IUserBL userBL,
            IMapper mapper,
            IEmailSender mailSender,
            IOptions<JWTSetttings> appSettings)
        {
            _userBL = userBL;
            _mapper = mapper;
            _mailSender = mailSender;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Models.LoginRequest login)
        {
            try
            {
                var token = await _userBL.CheckLogin(login);
                return Ok(new { token });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message.ToString() });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Models.RegisterRequest register)
        {
            Entities.User user = null;
            var isCreated = false;
            try
            {
                user = _mapper.Map<Entities.User>(register);
                isCreated = await _userBL.CreateUser(user);
                if (isCreated)
                {
                    //await _mailSender.SendEmailAsync(user.Email, "Created Account", "Your password: " + password);
                }
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

            }
            catch (DulicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception)
            {
                if (isCreated)
                {
                    //await _bl.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
            }
        }
    }
}