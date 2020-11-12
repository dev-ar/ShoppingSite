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
        [Route("AddPG/{id}")]
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
        [Route("AddPG/{id}")]
        public ActionResult Create(int id, [Bind(Include = "GroupTitle")] AddProductGroupsViewModel model)
        {
            if (!ModelState.IsValid) return PartialView(model);

            var PG = new ProductGroups {GroupTitle = model.GroupTitle};
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
            return PartialView("ListGroups",db.ProductGroupsRepository.GetAll());
        }

        //GET: Admin/ProductGroups/Edit/5
        [Route("EditPG/{id}")]
        public ActionResult Edit(int id)
        {
            var productGroups = db.ProductGroupsRepository.GetById(id);

            if (productGroups == null)
            {
                return HttpNotFound();
            }

            //var pgViewModel = new AddProductGroupsViewModel
            //{
            //    GroupTitle = productGroups.GroupTitle
            //};
            return PartialView(productGroups);
        }

        //POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditPG/{id}")]
        public ActionResult Edit([Bind(Include = "GroupId,GroupTitle,ParentId")] ProductGroups productGroups)
        {
            if (!ModelState.IsValid) return PartialView(productGroups);

            db.ProductGroupsRepository.Update(productGroups);
            db.Save();
            return PartialView("ListGroups", db.ProductGroupsRepository.GetAll());
        }

        // GET: Admin/ProductGroups/Delete/5
        [Route("DeletePG/{id}")]
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

        //// POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("DeletePG/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var pg = db.ProductGroupsRepository.GetById(id);
            if (pg.ProductGroups1.Any())
            {
                var secSubGroups = db.AccountRepository.GetAllSubGroups(id).ToList();
                foreach (var sec in secSubGroups)
                {
                    var thirdSubGroups = db.AccountRepository.GetAllSubGroups(sec.GroupId);
                    foreach (var third in thirdSubGroups)
                    {
                        db.ProductGroupsRepository.Delete(third);
                    }

                    db.ProductGroupsRepository.Delete(sec);
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
