using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.Dbo.Base;

namespace PavlicWebShop.Models.Dbo
{
    public class ProductCategory : ProductCategoryBase, IEntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
