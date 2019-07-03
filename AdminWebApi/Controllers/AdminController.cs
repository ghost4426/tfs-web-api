using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Common.Constant;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common.Utils;
using DTO.Models.Exception;
using AutoMapper;

namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = RoleConstant.ADMIN)]
    public class AdminController : ControllerBase
    {

        private readonly IUserBL _userBL;
        private IMapper _mapper;
        private IEmailSender _mailSender;

        public AdminController(IUserBL UserBL,
            IMapper mapper,
            IEmailSender mailSender
            )
        {
            _userBL = UserBL;
            _mapper = mapper;
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
                user = _mapper.Map<Entities.User>(rUser);
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
                    await _mailSender.SendEmailAsync(user.Email, "Created Account", "Your password: " + password);
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
                    await _userBL.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
            }

        }

        [HttpGet("Users")]
        public async Task<IList<Entities.User>> Users()
        {
            return await _userBL.GetUsers();
        }

        //GET : /api/admin/profile
        //[Authorize(Roles = RoleConstant.ADMIN)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var claim = User.Claims;
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userBL.GetById(int.Parse(userId));
            return Ok(user);
        }
        [HttpGet("User/{userId}")]
        public async Task<Entities.User> Get1Users(int userId)
        {
            var user = await _userBL.GetById(userId);
            return user;

        }
        [HttpPut("Users/Update/{id}")]
        public async Task<Models.UpdateUserReponse> UpdateUser(int id, [FromBody] Models.UpdateUserRequest userInfo)
        {
            Entities.User user = new Entities.User()
            {
                UserId = id,
                Fullname = userInfo.fullName,
                Email = userInfo.email,
                PhoneNo = userInfo.phone,
            };
            await _userBL.UpdateUser(user, 3);
            var reponseModel = new Models.UpdateUserReponse()
            {
                UserId = user.UserId
            };
            return reponseModel;

        }
        [HttpPut("User/Role/{id}")]
        public async Task<IActionResult> Role(int id, [FromBody] string role)
        {
            try
            {
                var userRole = await _userBL.ChangeRole1User(id, int.Parse(role));
                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message.ToString() });

            }
        }

        [HttpPut("User/Deactive/{userId}")]
        public async Task<IActionResult> Deactive(int userId)
        {
            try
            {
                await _userBL.updateUserStatus(userId);
                return Ok("Success!");
            }
            catch (NotFoundException e)
            {
                return BadRequest(new { message = e.Message.ToString() });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message.ToString() });
            }
        }
    }
}



