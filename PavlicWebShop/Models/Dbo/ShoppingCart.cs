using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.Dbo.Base;

namespace PavlicWebShop.Models.Dbo
{
    public class ShoppingCart : ShoppingChartBase, IEntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ShoppingCartStatus ShoppingCartStatus { get; set; }
    }
}
