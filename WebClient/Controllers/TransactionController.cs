﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("TransactionManagement");
        }

        [Route("quan-li-giao-dich")]
        public IActionResult TransactionManagement()
        {
            return View();
        }
    }
}