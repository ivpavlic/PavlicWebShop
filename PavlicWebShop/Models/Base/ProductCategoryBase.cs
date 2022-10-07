using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Models.Base
{
    public class ProductCategoryBase
    {
        [Required(ErrorMessage = "Required field!")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Required field!")]
        public string Description { get; set; }
    }
}
