﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class tstController : Controller
    {
        // GET: tst
        [Authorize(Roles = "admin")]
        public ActionResult test()
        {
            return View();
        }
    }
}