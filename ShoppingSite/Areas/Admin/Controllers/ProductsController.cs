﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Domain;
using InsertShowImage;
using Utilities;
using ViewModel;

namespace ShoppingSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [RouteArea("Admin", AreaPrefix = "admin")]
    public class ProductsController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());

        #region Product

        [Route("Products")]
        public ActionResult Index()
        {
            ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
            ViewBag.SelectedPG = db.SelectedProductGroupRepository.GetAll();
            return View(db.ProductsRepository.GetAll());
        }

        // GET: Admin/Products/Details/5
        [Route("Products/Detail/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductsRepository.GetById(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        [Route("Products/Add")]
        public ActionResult Create()
        {
            ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Products/Add")]
        public ActionResult Create([Bind(Include = "ProductTitle,ShortDescription,Description,Price,Tags")] AddProductsViewModel model,
            HttpPostedFileBase imageProduct, List<int> selectedPg)
        {
            if (ModelState.IsValid)
            {
                if (selectedPg == null)
                {
                    ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
                    ViewBag.ErrorPG = true;
                    return View(model);
                }

                var product = new Products
                {
                    ProductTitle = model.ProductTitle,
                    ShortDescription = model.ShortDescription,
                    Description = model.Description,
                    Price = model.Price,
                    ProductRate = 0,
                    CreateDate = DateTime.Now
                };



                //Insert Tags
                if (string.IsNullOrEmpty(model.Tags) == false)
                {
                    var tags = model.Tags.Split('.');
                    foreach (var tag in tags)
                    {
                        if (string.IsNullOrEmpty(tag))
                        {
                            ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
                            ModelState.AddModelError("Tags", "لطفا کلمات کلیدی را به درستی با نقطه(.) جدا کنید.");
                            return View(model);
                        }

                        db.ProductTagsRepository.Insert(new ProductTags
                        {
                            ProductId = product.ProductId,
                            Tag = tag.Trim()
                        });
                    }
                }

                product.ImageName = "Default.png";
                if (imageProduct != null && imageProduct.IsImage())
                    product.ImageName = Guid.NewGuid() + Path.GetExtension(imageProduct.FileName);

                db.ProductsRepository.Insert(product);

                foreach (var item in selectedPg)
                {
                    db.SelectedProductGroupRepository.Insert(new SelectedProductGroup()
                    {
                        GroupId = item,
                        ProductId = product.ProductId
                    });
                }

                db.Save();

                if (product.ImageName != "Default.png")
                {
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + product.ImageName));
                    var img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + product.ImageName),
                        Server.MapPath("/Images/ProductImages/Resized/" + product.ImageName));
                }

                return RedirectToAction("Index");
            }

            ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
            return View(model);
        }

        [Route("Products/Edit/{id}")]
        public ActionResult Edit(int id)
        {

            var product = db.ProductsRepository.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var editProduct = new AddProductsViewModel
            {
                ProductId = id,
                ProductTitle = product.ProductTitle,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                Tags = string.Join(".", product.ProductTags.Select(t => t.Tag))
            };

            ViewBag.ImageName = product.ImageName;
            ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
            return View(editProduct);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Products/Edit/{id}")]
        public ActionResult Edit([Bind(Include = "ProductTitle,ShortDescription,Description,Price,Tags,ProductId")] AddProductsViewModel model, HttpPostedFileBase imageProduct, List<int> selectedPg, int id)
        {
            if (ModelState.IsValid)
            {
                if (selectedPg == null)
                {
                    ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
                    ViewBag.ErrorPG = true;
                    return View(model);
                }

                //Product info Edit
                var product = db.ProductsRepository.GetById(model.ProductId);
                product.ProductTitle = model.ProductTitle;
                product.Description = model.Description;
                product.ShortDescription = model.ShortDescription;
                product.Price = model.Price;

                //Image Edit
                if (imageProduct != null && imageProduct.IsImage())
                {
                    if (product.ImageName != "Default.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + product.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Resized/" + product.ImageName));
                    }

                    var imageName = Guid.NewGuid() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + imageName));
                    var img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + imageName),
                        Server.MapPath("/Images/ProductImages/Resized/" + imageName));
                    product.ImageName = imageName;
                }

                //Tags Edit
                var strTag = string.Join(".", product.ProductTags.Select(t => t.Tag));
                if (string.IsNullOrEmpty(model.Tags) == false && strTag != model.Tags)
                {

                    foreach (var tag in product.ProductTags.ToList())
                        db.ProductTagsRepository.Delete(tag);


                    var newTags = model.Tags.Split('.');
                    foreach (var tag in newTags)
                    {
                        db.ProductTagsRepository.Insert(new ProductTags
                        {
                            ProductId = product.ProductId,
                            Tag = tag.Trim()
                        });
                    }


                }

                //Delete The Groups
                var selectedGroups = product.SelectedProductGroups.ToList();
                foreach (var group in selectedGroups)
                {
                    foreach (var item in selectedPg)
                    {
                        if (item == group.GroupId)
                        {
                            goto loop;
                        }
                    }

                    db.SelectedProductGroupRepository.Delete(group);

                loop:;
                }

                //Insert The Groups
                foreach (var item in selectedPg)
                {
                    foreach (var group in selectedGroups)
                    {
                        if (item == group.GroupId)
                        {
                            goto loop;
                        }
                    }

                    db.SelectedProductGroupRepository.Insert(new SelectedProductGroup()
                    {
                        GroupId = item,
                        ProductId = product.ProductId
                    });

                loop:;
                }

                db.Save();

                return RedirectToAction("Index");
            }
            ViewBag.ProductGroups = db.ProductGroupsRepository.GetAll();
            return View(model);
        }

        [Route("Products/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.ProductsRepository.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Products/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {

            var product = db.ProductsRepository.GetById(id);

            //Delete Selected Groups
            foreach (var item in product.SelectedProductGroups.ToList())
                db.SelectedProductGroupRepository.Delete(item);

            //Delete Tags
            foreach (var item in product.ProductTags.ToList())
                db.ProductTagsRepository.Delete(item);

            //Delete Photos
            if (product.ImageName != "Default.png")
            {
                System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + product.ImageName));
                System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Resized/" + product.ImageName));
            }

            //Delete Gallery
            if (product.ProductGalleries.Any())
            {
                foreach (var item in product.ProductGalleries.ToList())
                {
                    System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + item.ImageName));
                    System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Resized/" + item.ImageName));
                    db.ProductGalleriesRepository.Delete(item);
                }
            }

            //Delete Features
            if (product.ProductFeatures.Any())
            {
                foreach (var item in product.ProductFeatures.ToList())
                    db.ProductFeaturesRepository.Delete(item);
            }

            db.ProductsRepository.Delete(product);

            db.Save();
            return RedirectToAction("Index");
        }

        #endregion

        #region Gallery
        [Route("Products/Gallery/{id}")]
        public ActionResult Gallery(int id)
        {
            var product = db.ProductsRepository.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Gallery = db.ProductGalleriesRepository.GetAll().Where(g => g.ProductId == id).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Products/Gallery/{id}")]
        public ActionResult Gallery(AddGalleryViewModel model, HttpPostedFileBase imgUp, int id)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null && imgUp.IsImage())
                {
                    var gallery = new ProductGalleries
                    {
                        ProductId = id,
                        GalleryTitle = model.GalleryTitle,
                        ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName)
                    };

                    imgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));

                    var img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + gallery.ImageName),
                        Server.MapPath("/Images/ProductImages/Resized/" + gallery.ImageName));

                    db.ProductGalleriesRepository.Insert(gallery);
                    db.Save();
                    RedirectToAction("Gallery", new { id = id });
                }
                else
                {
                    return Redirect("/Admin/Products/Gallery/" + id + "?img=error");
                }
            }

            return RedirectToAction("Gallery", new { id = id });
        }


        [Route("Products/Gallery/Delete/{id}")]
        public ActionResult DeleteGallery(int id)
        {
            var gallery = db.ProductGalleriesRepository.GetById(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }

            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Resized/" + gallery.ImageName));

            db.ProductGalleriesRepository.Delete(gallery);
            db.Save();

            return RedirectToAction("Gallery", new { id = gallery.ProductId });
        }


        #endregion

        #region Features

        [Route("Products/Features/{id}")]
        public ActionResult ProductFeatures(int id)
        {
            var product = db.ProductsRepository.GetById(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var selectedPgs = product.SelectedProductGroups.Select(t => t.GroupId).ToList();

            //Insert Public and Related Features
            var features = db.FeaturesRepository.GetAll().Where(f => f.GroupId == null).ToList();
            features.AddRange(selectedPgs.SelectMany(item => db.FeaturesRepository.GetAll().Where(g => g.GroupId == item)));


            ViewBag.Features = db.ProductFeaturesRepository.GetAll().Where(f => f.ProductId == id).ToList();
            ViewBag.FeatureId = new SelectList(features, "FeatureId", "FeatureTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Products/Features/{id}")]
        public ActionResult ProductFeatures(int id, ProductFeatures model)
        {
            if (ModelState.IsValid)
            {
                model.ProductId = id;
                db.ProductFeaturesRepository.Insert(model);
                db.Save();
                return RedirectToAction("ProductFeatures", new { id = id });
            }

            return RedirectToAction("ProductFeatures", new { id = id });
        }

        #endregion




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        [Route("GetSelectedPG/{id}")]
        public ActionResult GetSelectedPGs(int id)
        {
            var data = db.AccountRepository.GetSelectedPGsByProductId(id)
                .Select(g => g.GroupId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
