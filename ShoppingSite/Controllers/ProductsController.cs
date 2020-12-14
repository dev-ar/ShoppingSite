#nullable enable
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            if (ModelState.IsValid == false)
                return RedirectToAction("ShowProduct", new { id = model.ProductId });


            var user = db.AccountRepository.GetUserByEmail(User.Identity.Name.ToLower());
            var comment = new ProductComments
            {
                CommentTitle = "Comment",
                CreateDate = DateTime.Now,
                Rate = (qualityRate + worthRate) / 2,
                Text = model.Text,
                ProductId = model.ProductId,
                UserId = user.UserId,
                ParentId = model.ParentId
            };


            db.ProductCommentsRepository.Insert(comment);

            var product = db.ProductsRepository.GetById(model.ProductId);
            product.ProductRate = product.ProductComments.Select(r => r.Rate).Sum() / product.ProductComments.Count;

            db.Save();

            var comments = product.ProductComments.AsEnumerable();

            if (model.ParentId != null)
                comments = comments.Reverse();

            return PartialView("ShowComments", comments);

        }


        [Route("Archive")]
        public ActionResult Filter(int pg = 0, int pageId = 1, int minPrice = 0, int maxPrice = 0, string title = "")
        {
            var products = db.ProductsRepository.GetAll().ToList();
            var list = new List<Products>();

            ViewBag.Titles = title;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.pageId = pageId;
            ViewBag.pg = pg;

            if (pg != 0)
            {
                var PG = db.ProductGroupsRepository.GetById(pg);
                if (PG == null)
                    return HttpNotFound();

                var allPgs = db.ProductsCustomRepository.GetRelatedPgs(PG);
                foreach (var item in allPgs)
                {
                    list.AddRange(products.Where(g => g.SelectedProductGroups
                        .Any(g => g.GroupId == item.GroupId)));
                }
            }
            else
            {
                list = products;
            }

            if (title != "")
            {
                list = list.Where(g => g.ProductTitle.Contains(title)).ToList();
            }

            if (minPrice != 0)
            {
                list = list.Where(p => p.Price >= minPrice).ToList();
            }

            if (maxPrice != 0)
            {
                list = list.Where(p => p.Price <= maxPrice).ToList();
            }


            

            //Paging
            const int take = 6;
            var skip = (pageId - 1) * take;
            ViewBag.pageCount = Math.Ceiling((decimal) list.Count() / take);

            var productList = list.OrderByDescending(p => p.CreateDate).Skip(skip).Take(take).Distinct().ToList();

            return View(productList);
        }

        
    }
}