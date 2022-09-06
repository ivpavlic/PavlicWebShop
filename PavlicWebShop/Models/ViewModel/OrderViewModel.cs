﻿using PavlicWebShop.Models.Base;

namespace PavlicWebShop.Models.ViewModel
{
    public class OrderViewModel : OrderBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public ShoppingCartViewModel ShoppingCart { get; set; }
    }
}
