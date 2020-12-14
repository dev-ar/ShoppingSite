using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ViewModel;

namespace ShoppingSite.Controllers
{
    public class ShoppingCartController : ApiController
    {
        // GET: api/ShoppingCart
        public int Get()
        {

            var list = new List<AddShopCartViewModel>();
            var sessions = HttpContext.Current.Session;


            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<AddShopCartViewModel>;
            }

            return list.Sum(l => l.Count);
        }

        // GET: api/ShoppingCart/5
        public int Get(int id)
        {
            var list = new List<AddShopCartViewModel>();
            var sessions = HttpContext.Current.Session;


            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<AddShopCartViewModel>;
            }

            if (list != null && list.Any(c => c.ProductId == id))
            {
                var index = list.FindIndex(c => c.ProductId == id);
                list[index].Count += 1;
            }
            else
            {
                var shopCart = new AddShopCartViewModel
                {
                    ProductId = id,
                    Count = 1
                };

                list.Add(shopCart);
            }

            sessions["ShopCart"] = list;
            return Get();
        }
    }
}
