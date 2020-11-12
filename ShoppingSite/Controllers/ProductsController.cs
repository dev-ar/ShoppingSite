using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;

namespace ShoppingSite.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        public ActionResult ShowGroups()
        {
            return PartialView(db.ProductGroupsRepository.GetAll());
        }
    }
}