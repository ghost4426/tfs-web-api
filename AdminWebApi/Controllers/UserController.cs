using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using Entities = DTO.Entities;
using BusinessLogic.IBusinessLogic;


namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserBL bl;

        public UserController(IUserBL UserBL)
        {
            this.bl = UserBL;
        }


        /// <summary>
        ///Create new User
        /// </summary>
        /// <returns></returns>
        [HttpPost("/createUser")]
        public async Task<Models.CreateUserReponse> CreateUser([FromBody] Models.CreateNewUserRequest newUser)
        {
            Entities.User user = new Entities.User() { Username = newUser.Username, Password = newUser.Password, RoleId = newUser.RoleId, IsActive = true };
            await bl.CreateUser(user);
            var reponseModel = new Models.CreateUserReponse()
            {
                UserId = user.UserId
            };
        return reponseModel;
        }

    }
}
