using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("nha-phan-phoi")]
    public class DistributorController : Controller
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
    }
}