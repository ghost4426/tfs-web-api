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
using DTO.Models.Exception;
using Microsoft.AspNetCore.Cors;


namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /*    [Authorize(Roles = RoleConstant.ADMIN)]*/

    public class AdminController : ControllerBase
    {

        private readonly IUserBL _bl;

        public AdminController(IUserBL UserBL)
        {
            _bl = UserBL;
        }

        //POST : /api/Admin/createUser
        //[Authorize(Roles = RoleConstant.ADMIN)]
        [HttpPost("createUser")]
        public async Task<Models.CreateUserReponse> CreateUser([FromBody] Models.CreateNewUserRequest newUser)
        {
            Entities.User user = new Entities.User() { Username = newUser.Username, Password = newUser.Password, RoleId = newUser.RoleId, IsActive = true };
            await _bl.CreateUser(user);
            var reponseModel = new Models.CreateUserReponse()
            {
                UserId = user.UserId
            };
            return reponseModel;
        }

        [HttpGet("/Users")]
        public async Task<IList<Entities.User>> Users()
        {
            return await _bl.GetUsers();
        }
        [HttpGet("/User/{userId}")]
        public async Task<Entities.User> Get1Users(int userId)
        {
            var user = await _bl.GetById(userId);
            return user;
        }
        [HttpPut("/Users/Update/{id}")]
        public async Task<Models.UpdateUserReponse> UpdateUser(int id,[FromBody] Models.UpdateUserRequest userInfo)
        {
            Entities.User user = new Entities.User()
            {
                UserId = id,
                Fullname = userInfo.fullName,
                Email = userInfo.email,
                PhoneNo = userInfo.phone,
            };
/*            var claim = User.Claims;
            string userId = User.Claims.First(c => c.Type == "UserID").Value;*/
            await _bl.UpdateUser(user, 3);
            var reponseModel = new Models.UpdateUserReponse()
            {
                UserId = user.UserId
            };
            return reponseModel;
            /*  var claim = User.Claims;
              string userId = User.Claims.First(c => c.Type == "UserID").Value;*/
            /*var userId = 7;
            if (id == userId)
            {
                var user = await _bl.GetById(id);
                return await _bl.UpdateUser(user, fullName, Email, phone);
            }
            else
            {
                return BadRequest(new { message = "Wrong User" });
            }*/

        }
        [HttpPut("/User/Role/{id}")]
        public async Task<IActionResult> Role(int id, [FromBody] string role)
        {
            try
            {
                var userRole = await _bl.ChangeRole1User(id, int.Parse(role));
                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message.ToString() });

            }
        }

        [HttpPut("/User/Deactive/{userId}")]
        public async Task<IActionResult> Deactive(int userId)
        {
            try
            {
                await _bl.updateUserStatus(userId);
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



