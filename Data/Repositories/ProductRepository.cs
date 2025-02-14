using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : IProductRepository
    {
        private static readonly IList<Product> Products = new List<Product>();
        
        public void Save(Product product)
        {
            var existingProduct = Get(product.Id);

            if (existingProduct != null)
            {
                Products.Remove(existingProduct);
            }

            Products.Add(product);
        }

        public void Delete(Product product)
        {
            Products.Remove(product);
        }

        public Product Get(Guid id)
        {
            return Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> Get(ProductTypes? productType = null, string name = null)
        {
            var result = Products;

            if(productType != null)
            {
                result = result.Where(x => x.Type == productType).ToList();
            }

            name = (name ?? "").Trim();
            if (name.Length > 0)
            {
                result = result.Where(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return result;
        }
        
        public void DeleteAll()
        {
            Products.Clear();
        }
    }
}