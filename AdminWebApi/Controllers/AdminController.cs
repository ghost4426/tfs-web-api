using BusinessLogic.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models = DTO.Models;
using System.Threading.Tasks;
using DTO.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AdminWebApi.Controllers
{
    public class AdminController : Controller
    {
        private IUserBL bl;

        public AdminController(IUserBL UserBL)
        {
            this.bl = UserBL;
        }

        [HttpPost("/Users")]
        public async Task<IList<User>> Users()
        {
            IList<User> users = await bl.GetUsers();
            //Tự gán list user vào useRespone . Db k trả về null

            /*var reponseModel = new Models.GetUserResponse()
            {
                User = IList< User > users = await bl.GetUsers()
        };*/
                return users;
        }
    }
}
