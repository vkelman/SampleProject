using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        /// <inheritdoc cref="IUpdateProductService.Update"/>>
        public Product Update(Product product, string name, string description, ProductTypes? type, decimal? price, bool ignoreNullValues = false)
        {
            name = (name + string.Empty).Trim();
            description = (description + string.Empty).Trim();

            //// Note: we assume that parameter name == null and name == string.Empty are equivalent and should be treated as "skip this parameter"
            if (!(ignoreNullValues && name.Length == 0)) product.SetName(name);
            if (!(ignoreNullValues && description.Length == 0))  product.SetDescription(description);
            // ReSharper disable once PossibleInvalidOperationException
            if (!(ignoreNullValues && type == null)) product.SetType(type.Value);
            if (!(ignoreNullValues && price == null)) product.SetPrice(price);

            return product;
        }
    }
}