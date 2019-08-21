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
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly IRoleBL _roleBl;
        private IMapper _mapper;
        private readonly IPremisesBL _premisesBL;
        private readonly IPremisesTypeBL _premisesTypeBL;

        public UserController(IUserBL UserBL,
             IRoleBL RoleBL,
             IMapper mapper,
             IEmailSender mailSender,
             IPremisesBL premisesBL,
             IPremisesTypeBL premisesTypeBL
             )
        {
            _roleBl = RoleBL;
            _userBL = UserBL;
            _mapper = mapper;
            _premisesBL = premisesBL;
            _premisesTypeBL = premisesTypeBL;
        }

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
            catch (Exception e)
            {
                return Ok(new { data = userData });
            }
            //return user;
            return Ok(new { data = userData });
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] Models.ChangePasswordUserRequest userInfo)
        {
            try
            {
                int userid = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _userBL.ChangePassword(userid, userInfo.newPass, userInfo.oldPass);
                return Ok("success!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("user/avatar")]
        public async Task<IActionResult> ChangeAvatar([FromBody] Models.ChangeAvatar ava)
        {

            try
            {
                int userId = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                await _userBL.ChangeAvatar(userId, ava.avaUrl);
                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message.ToString() });
            }
        }

        [HttpPut("users/update")]
        public async Task<IActionResult> UpdateUser([FromBody] Models.UpdateUserRequest userInfo)
        {
            Entities.User user = null;
            try
            {
                var id = int.Parse(User.Claims.First(c => c.Type == "userID").Value);
                user = _mapper.Map<Entities.User>(userInfo);
                user.UserId = id;
                user.Fullname = userInfo.Fullname;
                user.Email = userInfo.Email;
                user.PhoneNo = userInfo.PhoneNo;
                await _userBL.UpdateUser(user, id);
                return Ok("success!!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }
    }
}