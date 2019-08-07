﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("nong-trai")]
    [Authorize(Roles = "Farm")]
    public class FarmController : Controller
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

        [Route("quan-li-tai-khoan")]
        public IActionResult AccountManagement()
        {
            return View();
        }
    }
}