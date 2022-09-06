using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.Dbo.Base;

namespace PavlicWebShop.Models.Dbo
{
    public class Order : OrderBase, IEntityBase
    {
        public ShoppingCart ShoppingCart { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
