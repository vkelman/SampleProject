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
            //// Note: we assume that parameter name == null and name == string.Empty are equivalent and should be treated as "skip this parameter"
            if (!ignoreNullValues || (name + string.Empty).Trim().Length != 0) product.SetName(name);
            if (!ignoreNullValues || (description + string.Empty).Trim().Length != 0) product.SetDescription(description);
            // ReSharper disable once PossibleInvalidOperationException
            if (!ignoreNullValues || type != null) product.SetType(type.Value);
            if (!ignoreNullValues || price != null) product.SetPrice(price);

            return product;
        }
    }
}