using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Description = product.Description;
            Type = new EnumData(product.Type);
            Price = product.Price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public EnumData Type { get; set; }
        public decimal? Price { get; set; }
    }
}