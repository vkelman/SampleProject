using BusinessEntities;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        /// <summary>
        /// Updates the product properties with the given values.
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="name">product name</param>
        /// <param name="description">product description</param>
        /// <param name="type">product type</param>
        /// <param name="price">product price</param>
        /// <param name="ignoreNullValues">If true, don't replace product properties with nulls (suitable for update operation with optional parameters;
        /// if false, raise a corresponding exception if some of the parameters are nulls (suitable for create operation).</param>
        /// <returns>updated Product object</returns>
        Product Update(Product product, string name, string description, ProductTypes? type, decimal? price, bool ignoreNullValues = false);
    }
}