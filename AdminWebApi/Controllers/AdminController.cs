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
        private readonly IRoleBL _roleBl;
        private IMapper _mapper;
        private IEmailSender _mailSender;

        public AdminController(IUserBL UserBL,
            IRoleBL RoleBL,
            IMapper mapper,
            IEmailSender mailSender
            )
        {
            _roleBl = RoleBL;
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
        [HttpPost("user")]
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
                user.Fullname = "thaihd";
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
            catch (Exception e)
            {
                if (isCreated)
                {
                    await _userBL.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = e.Message });
            }

        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            return Ok(new { data = _mapper.Map<IList<Models.User>>(await _userBL.GetUsers()) });
        }

        [HttpPut("password/{userid}")]
        public async Task<IActionResult> ChangePassword(int userid, [FromBody] Models.UpdateUserRequest userInfo)
        {
            try
            {
                await _userBL.ChangePassword(userid, userInfo.newPass, userInfo.oldPass);
                return Ok("success!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

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
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> Get1Users(int userId)
        {
            return Ok(new { data = _mapper.Map<Entities.User>(await _userBL.GetById(userId)) });
        }
        [HttpGet("role")]
        public async Task<IList<Entities.Role>> GetRole()
        {
            var roleList = await _roleBl.GetAllRole();
            return roleList;

        }
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetRoleInfo(int roleId)
        {
            var role = await _roleBl.GetById(roleId);
            return Ok(role);

        }
        [HttpPut("users/update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Models.UpdateUserRequest userInfo)
        {
            Entities.User user = new Entities.User()
            {
                UserId = id,
                Fullname = userInfo.fullName,
                Email = userInfo.email,
                PhoneNo = userInfo.phone,
            };
            await _userBL.UpdateUser(user, 16);
            return Ok();

        }
        [HttpPut("user/role/{id}")]
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

        [HttpPut("user/deactive/{userId}")]
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



