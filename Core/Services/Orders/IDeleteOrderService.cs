﻿using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        void Delete(Order user);
        void DeleteAll();
    }
}