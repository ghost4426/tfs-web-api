using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebClient.Controllers
{
    [Route("tai-khoan")]
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Route("cap-nhat-thong-tin")]
        public IActionResult UpdateProfile()
        {
            return View();
        }
        [Route("dang-ky-co-so")]
        public IActionResult CreatePremises()
        {
            return View();
        }
    }
}
