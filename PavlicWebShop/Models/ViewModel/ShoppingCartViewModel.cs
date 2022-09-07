﻿using PavlicWebShop.Models.Base;

namespace PavlicWebShop.Models.ViewModel
{
    public class ShoppingCartViewModel : ShoppingChartBase
    {
        public int Id { get; set; }
        public List<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }
        public ApplicationUserViewModel ApplicationUser { get; set; }
        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            if (ApplicationUser == null)
            {
                return default;
            }
            if (ShoppingCartItems.Any())
            {
                foreach (var item in ShoppingCartItems)
                {
                    totalPrice += item.Price * item.Quantity;
                }

            }

            return totalPrice;
        }

        public decimal GetTotalPriceWithVAT()
        {
            decimal totalPrice = GetTotalPrice();
            if (totalPrice == default)
            {
                return default;
            }


            totalPrice = totalPrice * 1.25M;
            return totalPrice;
        }

        public ShoppingCartStatus ShoppingCartStatus { get; set; }
        public DateTime Created { get; set; }
    }
}
