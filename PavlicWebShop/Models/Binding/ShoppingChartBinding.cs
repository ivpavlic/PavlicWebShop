using PavlicWebShop.Models.Base;

namespace PavlicWebShop.Models.Binding
{
    public class ShoppingChartBinding : ShoppingChartBase
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public int? ShoppingCartId { get; set; }
    }
}
