﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AddShopCartViewModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }

    public class ShowOrderViewModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }
    }

}
