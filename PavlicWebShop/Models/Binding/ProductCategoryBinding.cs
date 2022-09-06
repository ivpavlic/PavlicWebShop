using PavlicWebShop.Models.Base;

namespace PavlicWebShop.Models.Binding
{
    public class ProductCategoryBinding : ProductCategoryBase
    {
    }

    public class ProductCategoryUpdateBinding : ProductCategoryBinding
    {
        public int Id { get; set; }
    }
}
