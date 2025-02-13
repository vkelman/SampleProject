using BusinessEntities;
using System;

namespace Core.Services.Products
{
    public class CreateProductService : ICreateProductService
    {
        public Product Create(Guid id, string name, string description, ProductTypes type, decimal? price)
        {

        }
    }
}