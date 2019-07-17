using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("nong-trai")]
    public class FarmerController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("FoodManagement");
        }

        [Route("quan-li-san-pham")]
        public IActionResult FoodManagement()
        {
            return View();
        }

        [Route("quan-li-giao-dich")]
        public IActionResult FarmTransaction()
        {
            return View();
        }
    }
}