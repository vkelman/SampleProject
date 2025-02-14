using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public Product Update(Product product, string name, string description, ProductTypes type, decimal? price)
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetType(type);
            product.SetPrice(price);

            return product;
        }
    }
}