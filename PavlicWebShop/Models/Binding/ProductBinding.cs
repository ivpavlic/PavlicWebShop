using PavlicWebShop.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.Binding
{
    public class ProductBinding : ProductBase
    {
        public int ProductCategoryId { get; set; }
        [Display(Name = "Slika proizvoda")]
        public IFormFile ProductImg { get; set; }
    }
}
