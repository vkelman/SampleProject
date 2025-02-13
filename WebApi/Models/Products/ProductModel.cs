using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductTypes Type { get; set; }
        public decimal? Price { get; set; }
    }
}