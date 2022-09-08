using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.ViewModel;

namespace PavlicWebShop.Models.Binding
{
    public class ProductUpdateBinding : ProductBase
    {
        public int Id { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public IFormFile ProductImg { get; set; }
    }
}
