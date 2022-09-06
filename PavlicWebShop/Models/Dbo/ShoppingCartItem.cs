using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.Dbo.Base;

namespace PavlicWebShop.Models.Dbo
{
    public class ShoppingCartItem : ShoppingChartItemBase, IEntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public Product Product { get; set; }
    }
}
