using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Domain;
using Utilities;
using ViewModel;

namespace ShoppingSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [RouteArea("Admin", AreaPrefix = "admin")]
    public class SlidersController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());

        // GET: Admin/Sliders
        [Route("Sliders")]
        public ActionResult Index()
        {
            return View(db.SliderRepository.GetAll());
        }


        // GET: Admin/Sliders/Create
        [Route("Sliders/Add")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sliders/Add")]
        public ActionResult Create([Bind(Include = "SliderTitle,IsActive,StartDate,EndDate,Url")] SlidersViewModel model
            ,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                string imageName;
                if (imgUp != null && imgUp.IsImage())
                {
                    imageName = Guid.NewGuid() + System.IO.Path.GetExtension(imgUp.FileName);
                }
                else
                {
                    ModelState.AddModelError("ImageName", "لطفا تصویر را انتخاب کنید.");
                    return View();
                }

                PersianCalendar pc = new PersianCalendar();
                //DateTime dt = new DateTime(model.StartDate.get, pc);

                var slider = new Slider
                {
                    SliderTitle = model.SliderTitle,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Url = model.Url,
                    ImageName = imageName,
                    IsActive = model.IsActive
                };

                db.SliderRepository.Insert(slider);
                db.Save();

                imgUp.SaveAs(Server.MapPath("/Images/Slider/" + slider.ImageName));


                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Sliders/Edit/5\
        [Route("Sliders/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Slider slider = db.Sliders.Find(id);
            //if (slider == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*slider*/);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sliders/Edit/{id}")]
        public ActionResult Edit([Bind(Include = "SliderId,SliderTitle,ImageName,StartDate,EndDate,IsActive,Url")] Slider slider)
        {
            if (ModelState.IsValid)
            {
            //    db.Entry(slider).State = EntityState.Modified;
            //    db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(/*slider*/);
        }

        // GET: Admin/Sliders/Delete/5
        [Route("Sliders/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Slider slider = db.Sliders.Find(id);
            //if (slider == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*slider*/);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Sliders/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Slider slider = db.Sliders.Find(id);
            //db.Sliders.Remove(slider);
            //db.SaveChanges();
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
