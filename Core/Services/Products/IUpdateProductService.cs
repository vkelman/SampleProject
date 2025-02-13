﻿using BusinessEntities;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string description, ProductTypes type, decimal? price);
    }
}