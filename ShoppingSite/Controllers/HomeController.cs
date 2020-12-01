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
        // GET: Home
        public ActionResult Index()
        {
            var products = db.ProductsRepository.GetAll().OrderBy(t => t.CreateDate).Reverse().Take(12);
            return View(products);
        }

        public ActionResult Slider()
        {
            return PartialView();
        }

   

    }
}