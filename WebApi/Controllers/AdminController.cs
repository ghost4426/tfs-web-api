﻿using Microsoft.AspNetCore.Mvc;
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
using System.Text;

namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = RoleDataConstant.ADMIN)]
    public class AdminController : ControllerBase
    {
        private readonly IRegisterInfoBL _regBl;
        private readonly IUserBL _userBL;
        private readonly IRoleBL _roleBl;
        private IMapper _mapper;
        private IEmailSender _mailSender;
        private readonly IPremisesBL _premisesBL;
        private readonly IPremisesTypeBL _premisesTypeBL;
        public AdminController(IUserBL UserBL,
            IRoleBL RoleBL,
            IMapper mapper,
            IEmailSender mailSender,
            IRegisterInfoBL regBl,
            IPremisesBL premisesBL,
            IPremisesTypeBL premisesTypeBL
            )
        {
            _regBl = regBl;
            _roleBl = RoleBL;
            _userBL = UserBL;
            _mapper = mapper;
            _mailSender = mailSender;
            _premisesBL = premisesBL;
            _premisesTypeBL = premisesTypeBL;
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
                
                user.Password = "admin";
                user.Fullname = "";
                isCreated = await _userBL.CreateUser(user);
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
                return BadRequest(new { message = e.Message });
            }
        }

        //[AllowAnonymous]
        //[HttpPost("user/admin")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    Entities.User user = null;
        //    var isCreated = false;
        //    try
        //    {
        //        var admin = new Entities.User()
        //        {
        //            Username = "admin",
        //            Password = "admin",
        //            Fullname = "Admin System",
        //            Email = "Admin@tfs.com",
        //            RoleId = 1
        //        };
                
        //        isCreated = await _userBL.CreateAdmin(admin);
                
        //        return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });

        //    }
        //    catch (DuplicatedUsernameException e)
        //    {
        //        return BadRequest(new { message = e.Message });
        //    }
        //    catch (Exception e)
        //    {
        //        if (isCreated)
        //        {
        //            await _userBL.RemoveByIdAsync(user.UserId);
        //        }
        //        return BadRequest(new { message = MessageConstant.UNHANDLE_ERROR });
        //    }
        //}

        [HttpPost("premises")]
        public async Task<IActionResult> CreatePremises()
        {
            try
            {
                Entities.User user = new Entities.User();
               
                user = new Entities.User()
                {
                    Username = "Provider2",
                    Email = "Provider@test.com",
                    Fullname = "Provider 2",
                    RoleId = 2,
                    PremisesId = 1
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
        [HttpPost("veterinary")]
        public async Task<IActionResult> CreateVeterinary([FromBody] Models.CreateVeterinaryRequest veterinary)
        {
            var isCreated = false;
            var user = _mapper.Map<Entities.User>(veterinary);
            try
            {
                user.RoleId = 4;
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
                    await _mailSender.SendEmailAsync(user.Email, "Tạo tài khoản TSF","Tài khoản kiểm dịch được tạo thành công \n" + "Tên tài khoản: " + user.Username + "\n" + "Mật khẩu: " + password);
                }
                return Ok(new { messsage = MessageConstant.INSERT_SUCCESS });
            }
            catch (DuplicatedUsernameException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (DuplicateEmailException e)
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
            UserDetails userData = _mapper.Map<Models.UserDetails>(user);
            try
            {
                int premisesId = int.Parse(User.Claims.First(c => c.Type == "premisesID").Value);
                var premises = await _premisesBL.GetById(premisesId);
                userData.PremisesName = premises.Name;
                var premisesType = await _premisesTypeBL.GetById(premises.TypeId);
                userData.PremisesType = premisesType.Name;
            }
            catch(Exception e)
            {
                return Ok(new { data = userData });
            }
            //return user;
            return Ok(new { data = userData } );
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
                new { data = _mapper.Map<IList<Models.PremisesReponse>>(await _premisesBL.getAllPremisesAsync()) }
                );
        }
        [HttpPut("premises/status/{premisesId}")]
        public async Task<IActionResult> ChangeStatusPremises(int premisesId)
        {
            try
            {
                await _premisesBL.updatePremisesStatus(premisesId);
                return Ok(new { messsage = MessageConstant.UPDATE_SUCCESS });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }        
    }
}



