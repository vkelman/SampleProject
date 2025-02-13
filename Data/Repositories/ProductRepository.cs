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
        private static IList<Product> products = new List<Product>();


        public void Save(Product product)
        {
            products.Add(product);
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Get(Guid id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> Get(ProductTypes? productType = null, string name = null)
        {
            var resut = products;

            if(productType != null)
            {
                resut = resut.Where(x => x.Type == productType).ToList();
            }

            name = (name ?? "").Trim();
            if (name.Length > 0)
            {
                resut = resut.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
            }

            return resut;
        }
        
        public void DeleteAll()
        {
            products.Clear();
        }
    }
}