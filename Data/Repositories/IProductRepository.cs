using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(ProductTypes? productType = null, string name = null);
        void DeleteAll();
    }
}