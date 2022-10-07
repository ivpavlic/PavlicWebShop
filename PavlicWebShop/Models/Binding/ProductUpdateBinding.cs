using PavlicWebShop.Models.Base;
using PavlicWebShop.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.Binding
{
    public class ProductUpdateBinding : ProductBase
    {
        public int Id { get; set; }

        [Display(Name = "Product Category")]
        public ProductCategoryViewModel ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public IFormFile ProductImg { get; set; }

    }
}
