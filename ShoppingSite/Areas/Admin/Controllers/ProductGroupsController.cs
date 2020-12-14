using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Data;
using Data.Context;
using Domain;
using ViewModel;

namespace ShoppingSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [RouteArea("Admin", AreaPrefix = "admin")]
    public class ProductGroupsController : Controller
    {

        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        // GET: Admin/ProductGroups
        [Route("ProductGroups")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("ListGroups")]
        public ActionResult ListGroups()
        {
            return PartialView(db.ProductGroupsRepository.GetAll());
        }
        // GET: Admin/ProductGroups/Create
        [Route("ProductGroups/Add/{id}")]
        public ActionResult Create(int? id)
        {
            if (db.ProductGroupsRepository.GetById(id) == null && id != 0)
            {
                return HttpNotFound();
            }
            return PartialView();
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ProductGroups/Add/{id}")]
        public ActionResult Create(int id, [Bind(Include = "GroupTitle")] AddProductGroupsViewModel model)
        {
            if (!ModelState.IsValid) return PartialView(model);

            var PG = new ProductGroups { GroupTitle = model.GroupTitle };
            if (id == 0)
            {
                PG.ParentId = null;
            }
            else
            {
                PG.ParentId = id;
            }
            db.ProductGroupsRepository.Insert(PG);
            db.Save();
            return PartialView("ListGroups", db.ProductGroupsRepository.GetAll());
        }

        [Route("ProductGroups/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var productGroups = db.ProductGroupsRepository.GetById(id);

            if (productGroups == null)
            {
                return HttpNotFound();
            }

            return PartialView(productGroups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ProductGroups/Edit/{id}")]
        public ActionResult Edit([Bind(Include = "GroupId,GroupTitle,ParentId")] ProductGroups productGroups)
        {
            if (!ModelState.IsValid) return PartialView(productGroups);

            db.ProductGroupsRepository.Update(productGroups);
            db.Save();
            return PartialView("ListGroups", db.ProductGroupsRepository.GetAll());
        }

        [Route("ProductGroups/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroups productGroups = db.ProductGroupsRepository.GetById(id);
            if (productGroups == null)
            {
                return HttpNotFound();
            }
            ViewBag.pgName = productGroups.GroupTitle;
            return PartialView();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("ProductGroups/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var pg = db.ProductGroupsRepository.GetById(id);

            //Deleting all the selected groups
            var allPgs = db.ProductsCustomRepository.GetRelatedPgs(pg);
            foreach (var subItem in allPgs)
            {
                foreach (var pgs in subItem.SelectedProductGroups.ToList())
                {
                    db.SelectedProductGroupRepository.Delete(pgs);
                }
            }

            //Delete the all the SubGroups
            var allSubGroups = db.ProductsCustomRepository.GetRelatedPgs(pg).ToList();
            if (allSubGroups.Any())
            {
                foreach (var item in allSubGroups)
                {
                    db.ProductGroupsRepository.Delete(item);
                }
            }


            db.ProductGroupsRepository.Delete(pg);
            db.Save();
            return PartialView("ListGroups", db.ProductGroupsRepository.GetAll());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
