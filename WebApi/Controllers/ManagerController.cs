using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Constant;
using DTO.Models.Exception;
using Common.Utils;

namespace CommonWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly IMapper _mapper;
        private IEmailSender _mailSender;

        public ManagerController(
            IUserBL userBL,
            IMapper mapper,
            IEmailSender mailSender)
        {
            _userBL = userBL;
            _mapper = mapper;
            _mailSender = mailSender;
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

        [HttpPost("premise/account")]
        public async Task<IActionResult> CreatePremises([FromBody]Models.CreateUserPremises createUserForm)
        {
            var isCreated = false;
            var user = _mapper.Map<Entities.User>(createUserForm);
            try
            {
                user.RoleId = 3;
                user.PremisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                var password = Util.GeneratePassword(new Models.PasswordOptions()
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = true
                });
                user.Password = password;
                isCreated = await _userBL.CreateUser(user);
                if (isCreated)
                {
                    await _mailSender.SendEmailAsync(user.Email, "Tạo tài khoản TSF", "Tên tài khoản: " + user.Username +"\n"+"Mật khẩu: "+password);
                }
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

            }
            catch (DuplicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch(DuplicateEmailException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                if (isCreated)
                {
                    await _userBL.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = e.Message });
            }
        }
    }
}