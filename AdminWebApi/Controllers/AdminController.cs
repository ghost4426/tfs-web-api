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
using Microsoft.AspNetCore.Authorization;
using DTO.Models;

namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = RoleConstant.ADMIN)]
    public class AdminController : ControllerBase
    {
        private readonly IRegisterInfoBL _regBl;
        private readonly IUserBL _userBL;
        private readonly IRoleBL _roleBl;
        private IMapper _mapper;
        private IEmailSender _mailSender;

        public AdminController(IUserBL UserBL,
            IRoleBL RoleBL,
            IMapper mapper,
            IEmailSender mailSender,
            IRegisterInfoBL regBl
            )
        {
            _regBl = regBl;
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
                //var password = Util.GeneratePassword(new Models.PasswordOptions()
                //{
                //    RequireDigit = true,
                //    RequiredLength = 8,
                //    RequireLowercase = true,
                //    RequireNonAlphanumeric = false,
                //    RequireUppercase = true
                //});
                user.Password = "admin";
                user.Fullname = "";
                isCreated = await _userBL.CreateUser(user);
                //if (isCreated)
                //{
                //    await _mailSender.SendEmailAsync(user.Email, "Created Account", "Your password: " + password);
                //}
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

            }
            catch (DuplicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                if (isCreated)
                {
                    await _userBL.RemoveByIdAsync(user.UserId);
                }
                return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
            }
        }

        [HttpPost("premises")]
        public async Task<IActionResult> CreatePremises()
        {
            try
            {
                Entities.User user = new Entities.User()
                {
                    Username = "Farm1",
                    Email = "Farm@test.com",
                    Fullname = "Farm",
                    RoleId = 2,
                    PremisesId = 1

                };
                user.Password = "123";
                await _userBL.CreateUser(user);
                user = new Entities.User()
                {
                    Username = "Provider1",
                    Email = "Provider@test.com",
                    Fullname = "Provider",
                    RoleId = 2,
                    PremisesId = 2
                };
                user.Password = "123";
                await _userBL.CreateUser(user);

                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

            }
            catch (DuplicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            return Ok(new { data = _mapper.Map<IList<Models.User>>(await _userBL.GetUsers()) });
        }

        [HttpPut("password/{userid}")]
        public async Task<IActionResult> ChangePassword(int userid, [FromBody] Models.ChangePasswordUserRequest userInfo)
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
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var claim = User.Claims;
            string userId = User.Claims.First(c => c.Type == "userID").Value;
            var user = await _userBL.GetById(int.Parse(userId));
            var role = await _roleBl.GetById(user.RoleId);
            user.Role = role;
            //return user;
            return Ok(new {data = _mapper.Map<Models.User>(user) });
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
        public async Task<IActionResult> UpdateUser(int id,[FromBody] Models.UpdateUserRequest userInfo)
        {
            Entities.User user = null;
            try
            {
                user = _mapper.Map<Entities.User>(userInfo);
                user.UserId = id;
                user.Fullname = userInfo.Fullname;
                user.Email = userInfo.Email;
                user.PhoneNo = userInfo.PhoneNo;
                await _userBL.UpdateUser(user, 16);
                return Ok("success!!");

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

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
        //Premises Management
        //Get All RegisterInfo
        [HttpGet("premises")]
        public async Task<IActionResult> Premises()
        {
            return Ok(
                new { data = _mapper.Map<IList<Models.RegisterInfo>>(await this._regBl.GetAllRegisterInfo()) }
                );
        }
        [HttpPut("premises/status/{regId}")]
        public async Task<IActionResult> ChangeStatusPremises(int regId,[FromBody] int isConfirm)
        {
            try
            {
                await _regBl.ChangeStatusRegisterInfo(regId, isConfirm);
                return Ok("success!");
            }
            catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}



