using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Domain;
using ViewModel;

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

        [Route("Product/{id}")]
        public ActionResult ShowProduct(int id)
        {
            var product = db.ProductsRepository.GetById(id);
            if (product == null)
                return HttpNotFound();

            ViewBag.Features = product.ProductFeatures.GroupBy(t => t.Features).Distinct();

            return View(product);
        }


        public ActionResult ShowComments(int id)
        {
            var comments = db.ProductCommentsRepository.GetAll().Where(c => c.ProductId == id).Reverse();
            return PartialView(comments);
        }
            







        public ActionResult AddComment(int id, int? commentId)
        {
            var model = new AddCommentViewModel
            {
                ProductId = id
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(AddCommentViewModel model, int qualityRate, int worthRate)
        {

            if (ModelState.IsValid)
            {
                var user = db.AccountRepository.GetUserByEmail(User.Identity.Name.ToLower());
                var comment = new ProductComments
                {
                    CommentTitle = "Comment",
                    CreateDate = DateTime.Now,
                    Rate = (qualityRate + worthRate) / 2,
                    Text = model.Text,
                    ProductId = model.ProductId,
                    UserId = user.UserId,
                };



                db.ProductCommentsRepository.Insert(comment);
                db.Save();


                var comments = db.ProductCommentsRepository.GetAll().Where(c => c.ProductId == model.ProductId)
                    .Reverse();

                return PartialView("ShowComments",comments);
            }


            return RedirectToAction("ShowProduct", new {id = model.ProductId});
        }
    }
}