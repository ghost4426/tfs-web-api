using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Common.Constant;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common.Utils;
using DTO.Models.Exception;

namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = RoleConstant.ADMIN)]
    public class AdminController : ControllerBase
    {
        private readonly IUserBL _bl;
        private IAutoMapConverter<Models.CreateUserRequest, Entities.User> _mapCreateUserRequestToEntity;
        private IEmailSender _mailSender;

        //private IAutoMapConverter<Models.Contact, Entities.Contact> mapModelToEntity;


        public AdminController(IUserBL UserBL,
            IAutoMapConverter<Models.CreateUserRequest, Entities.User> mapCreateUserRequestToEntity,
            IEmailSender mailSender
            )
        {
            _bl = UserBL;
            _mapCreateUserRequestToEntity = mapCreateUserRequestToEntity;
            _mailSender = mailSender;
        }


        /// <summary>
        /// create new user
        /// </summary>
        /// <param name="rUser"></param>
        /// <returns></returns>
        //POST : /api/Admin/createUser
        //[Authorize(Roles = RoleConstant.ADMIN)]
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] Models.CreateUserRequest rUser)
        {
            Entities.User user = null;
            var isCreated = false;
            try
            {
                user = _mapCreateUserRequestToEntity.ConvertObject(rUser);
                var password = Util.GeneratePassword(new Models.PasswordOptions()
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = true
                });
                user.Password = password;
                isCreated = await _bl.CreateUser(user);
                if (isCreated)
                {
                   await _mailSender.SendEmailAsync(user.Email, "Created Account", "Your password: " + password);
                }
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

            }
            catch(DulicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message});
            }
            catch (Exception)
            {
                if (isCreated)
                {
                    await _bl.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
            }

        }

        //GET : /api/admin/profile
        //[Authorize(Roles = RoleConstant.ADMIN)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var claim = User.Claims;
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _bl.GetById(int.Parse(userId));
            return Ok(user);
        }

        [HttpPost("/Users")]
        public async Task<IList<Entities.User>> Users()
        {
            return await _bl.GetUsers();
        }
    }
}
