using PavlicWebShop.Models.Base;

namespace PavlicWebShop.Models.ViewModel
{
    public class ProductViewModel : ProductBase
    {
        public int Id { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }
    }
}
