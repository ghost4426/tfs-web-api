﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("tai-khoan")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("UserManagement");
        }
        [Route("quan-ly-tai-khoan")]
        public IActionResult UserManagement()
        {
            return View();
        }
    }
}