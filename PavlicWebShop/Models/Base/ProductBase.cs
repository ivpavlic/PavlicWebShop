using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PavlicWebShop.Models.Base
{
    public class ProductBase
    {
        [Required(ErrorMessage = "Required field!")]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Required field!")]
        //[DisplayFormat(DataFormatString = "{0.00}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Required field!")]
        //[DisplayFormat(DataFormatString = "{0.00}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(9, 2)")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public string? ProductImgUrl { get; set; }
    }
}
