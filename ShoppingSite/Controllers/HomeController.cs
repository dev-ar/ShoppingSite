using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;

namespace ShoppingSite.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        ShopSiteDB dbs = new ShopSiteDB();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

   

    }
}