using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebClient.Controllers
{
    [Route("tai-khoan")]
    [Authorize]
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Route("cap-nhat-thong-tin")]
        public IActionResult UserProfile()
        {
            return View();
        }
        
    }
}
