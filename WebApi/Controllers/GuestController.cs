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

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IRoleBL _roleBL;
        private readonly IUserBL _userBL;
        private readonly IFoodDataBL _foodDataBL;
        private readonly IMapper _mapper;
        private readonly IEmailSender _mailSender;
        private readonly JWTSetttings _appSettings;

        public GuestController(
            IUserBL userBL,
            IFoodDataBL foodDataBL,
            IMapper mapper,
            IEmailSender mailSender,
            IOptions<JWTSetttings> appSettings)
        {
            _userBL = userBL;
            _foodDataBL = foodDataBL;
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
                Entities.User user = null;
                if (token != null)
                {
                    user = await _userBL.FindByName(login.Username);
                }
                var loginReponse = new Models.UserLoginReponse()
                {
                    User = _mapper.Map<Models.UserData>(user),
                    Token = token
                };
                return Ok(new { Data = loginReponse });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, error = e.StackTrace });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Models.RegisterRequest register)
        {
            var user = _mapper.Map<Entities.User>(register);
            var premises = _mapper.Map<Entities.Premises>(register);
            var isCreated = false;
            try
            {                
                isCreated = await _userBL.Register(user,premises);
                if (isCreated)
                {
                    await _mailSender.SendEmailAsync(user.Email, "Tạo tài khoản TSF", "Bạn đã tạo tài khoản: " + user.Username +" thành công \n Mã kích hoạt: "+ user.ActivationCode);
                }
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

            }
            catch (DuplicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch(DuplicateEmailException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                if (isCreated)
                {
                    await _userBL.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = e.StackTrace });
            }
        }

        [HttpGet("foodData")]
        public async Task<IActionResult> GetFoodDataById(long id)
        {
            try
            {
                return Ok(new { data = await _foodDataBL.GetFoodDataByID(id) });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR, error = e.StackTrace });
            }
        }
        [HttpPut("account/activate/{activateCode}")]
        public async Task<IActionResult> ActivateAccount(string activateCode)
        {
            try
            {
                await _userBL.ActivateAccount(activateCode);
                return Ok(new { messsage = "Tài khoản đã được kích hoạt thành công" });
            }
            catch(NotFoundException e)
            {
                return BadRequest(new { message = e.Message});
            }
            catch(Exception e)
            {
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
            }
            
        }

        [HttpPut("forgetPassword")]
        public async Task<IActionResult> resetPassword([FromBody]Models.ForgetPasswordRequest forget)
        {
            var email = _mapper.Map<Entities.User>(forget);
            try
            {
                await _userBL.resetPassword(email.Email);
                return Ok(new { message = "Khôi phục mật khẩu thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}