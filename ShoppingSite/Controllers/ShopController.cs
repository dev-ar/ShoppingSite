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
    public class ShopController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        // GET: Shop
        public ActionResult Index()
        {
            return View("OrdersIndex");
        }



        public ActionResult Orders()
        {
            var list = GetOrderList();
            if (list == null || list.Any() == false)
            {
                ViewBag.NoProduct = true;
                return PartialView();
            }

            return PartialView(list);
        }

        [Route("Orders/Commands/{id}")]
        public ActionResult OrderCommands(int id, int count)
        {
            if (Session["ShopCart"] is List<AddShopCartViewModel> list)
            {
                var product = list.Find(t => t.ProductId == id);

                if (count != 0)
                {
                    product.Count = count;
                }
                else
                {
                    list.Remove(product);
                }

                Session["ShopCart"] = list;
            }

            return PartialView("Orders", GetOrderList());
        }

        [Authorize]
        public ActionResult Payment(int id)
        {
            var user = db.AccountRepository.GetUserByEmail(User.Identity.Name);

            var order = new Orders
            {
                UserId = user.UserId,
                AddressId = id,
                IsFinal = false,
                OrderDate = DateTime.Now
            };
            db.OrdersRepository.Insert(order);

            var list = GetOrderList();

            foreach (var item in list)
            {
                var orderDetails = new OrderDetails
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Count = item.Count,

                };
                db.OrderDetailsRepository.Insert(orderDetails);
            }
            db.Save();


            Session["ShopCart"] = new List<AddShopCartViewModel>();

            //ToDo: Return to Online payment sites

            return Redirect("/");
        }

        [Authorize]
        [Route("Shipping")]
        public ActionResult ChooseAddress()
        {
            var user = db.AccountRepository.GetUserByEmail(User.Identity.Name);
            var list = GetOrderList();



            if (list == null || list.Any() == false)
            {
                ViewBag.NoProduct = true;
                return View();
            }

            ViewBag.Orders = list;

            return View(user.Addresses.ToList());
        }

        [HttpPost]
        [Route("Shipping")]
        public ActionResult ChooseAddress(int? id)
        {

            if (id == null || db.AddressesRepository.GetById(id) == null)
            {
                var user = db.AccountRepository.GetUserByEmail(User.Identity.Name);
                ViewBag.Orders = GetOrderList();
                ViewBag.AddressError = true;
                return View(user.Addresses.ToList());
            }
       



            return RedirectToAction("Payment", new { id = id });
        }


        List<ShowOrderViewModel> GetOrderList()
        {
            var ordersList = new List<ShowOrderViewModel>();

            if (Session["ShopCart"] is List<AddShopCartViewModel> list)
            {
                foreach (var item in list)
                {
                    var product = db.ProductsRepository.GetById(item.ProductId);
                    var showOrder = new ShowOrderViewModel
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        ImageName = product.ImageName,
                        Price = product.Price,
                        Title = product.ProductTitle,
                        Sum = item.Count * product.Price

                    };

                    ordersList.Add(showOrder);
                }
            }

            return ordersList;
        }

    }
}