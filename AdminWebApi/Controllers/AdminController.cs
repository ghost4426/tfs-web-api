using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Common.Constant;
using System;
using System.Linq;

namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = RoleConstant.ADMIN)]
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

        //GET : /api/admin/profile
        //[Authorize(Roles = RoleConstant.ADMIN)]
        [HttpGet("profile")]
        public async Task<Object> GetUserProfile()
        {
            var claim = User.Claims;
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _bl.GetById(int.Parse(userId));
            return user;
        }
        [HttpPost("/Users")]
        public async Task<IList<User>> Users()
        {
            IList<User> users = await bl.GetUsers();
                return users;
        }
    }
}
