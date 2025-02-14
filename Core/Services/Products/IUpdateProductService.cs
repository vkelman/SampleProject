using BusinessEntities;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        Product Update(Product product, string name, string description, ProductTypes type, decimal? price);
    }
}