using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;

namespace ShoppingSite.Controllers
{
    public class SearchController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());

        [Route("Search")]
        public ActionResult Search(string q)
        {
            ViewBag.search = q;
            var products = db.ProductsCustomRepository.Search(q);
            return View(products);
        }
        
    }
}
