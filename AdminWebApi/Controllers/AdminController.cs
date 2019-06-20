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
        public async Task<IList<Entities.User>> Users()
        {
          return  await _bl.GetUsers();
        }

        [HttpPut("/Users/Update/{id}")]
        public void Put(int id, [FromBody] string value)
        {
            IList<User> users = await _bl.GetUsers();
            //Tự gán list user vào useRespone . Db k trả về null


        }
        [HttpPut("/User/Role/{id}")]
        public async Task<string> Role(int id, [FromBody] int role)
        {

            return await _bl.changeRole1User(id,role);
            /*var reponseModel = new Models.GetUserResponse()
            {
                User = IList< User > users = await bl.GetUsers()
        };*/
            
        }

        [HttpPut("/User/Update/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
