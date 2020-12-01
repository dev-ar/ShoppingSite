using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Domain;

namespace ShoppingSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [RouteArea("Admin", AreaPrefix = "admin")]
    public class FeaturesController : Controller
    {
        
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());

        [Route("Features")]
        public ActionResult Index()
        {
            
            return View(db.FeaturesRepository.GetAll());
        }


        // GET: Admin/Features/Create
        [Route("Features/Add")]
        public ActionResult Create()
        {
            var groups = db.AccountRepository.GetSelectableGroups().ToList();

            var pgPublic = new ProductGroups
            {
                GroupId = 0,
                GroupTitle = "عمومی",
            };
            groups.Add(pgPublic);

            ViewBag.GroupId = new SelectList(groups, "GroupId", "GroupTitle", 0);
            return PartialView();
        }

        // POST: Admin/Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Features/Add")]
        public ActionResult Create([Bind(Include = "FeatureId,FeatureTitle,GroupId")] Features model)
        {
            if (ModelState.IsValid)
            {
                if (model.GroupId == 0)
                {
                    model.GroupId = null;
                }

                db.FeaturesRepository.Insert(model);
                db.Save();
            
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        [Route("Features/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var features = db.FeaturesRepository.GetById(id);
            if (features == null)
            {
                return HttpNotFound();
            }

            features.GroupId ??= 0;


            var groups = db.AccountRepository.GetSelectableGroups().ToList();
            var pgPublic = new ProductGroups
            {
                GroupId = 0,
                GroupTitle = "عمومی",
            };
            groups.Add(pgPublic);

            ViewBag.GroupId = new SelectList(groups, "GroupId", "GroupTitle", features.GroupId);
            return PartialView(features);
        }

        //POST: Admin/Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Features/Edit/{id}")]
        public ActionResult Edit([Bind(Include = "FeatureId,FeatureTitle,GroupId")] Features features)
        {
            if (ModelState.IsValid)
            {
                if (features.GroupId == 0)
                {
                    features.GroupId = null;
                }


                db.FeaturesRepository.Update(features);
                db.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.GroupId = new SelectList(db.ProductGroups, "GroupId", "GroupTitle", features.GroupId);
            return RedirectToAction("Create");
        }

        // GET: Admin/Features/Delete/5
        [Route("Features/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var features = db.FeaturesRepository.GetById(id);
            if (features == null)
            {
                return HttpNotFound();
            }
            return PartialView(features);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Features/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.FeaturesRepository.Delete(id);
            db.Save();
            return RedirectToAction("Index");
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
