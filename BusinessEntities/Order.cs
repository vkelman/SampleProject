using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Represents an Order entity.
    /// </summary>
    public class Order : IdObject
    {
        private DateTime _orderDate;
        private OrderStatus _status;
        private User _user;
        private Dictionary<Product, int> _products = new Dictionary<Product, int>();

        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }


        public void SetOrderDate(DateTime? orderDate)
        {
            if (orderDate == null)
            {
                throw new ArgumentNullException(nameof(orderDate), "Order Date was not provided.");
            }
            if (orderDate < BusinessEntities.Constants.BusinessConstants.EarliestOrderDate)
            {
                throw new ArgumentOutOfRangeException(nameof(orderDate), "Order Date cannot be earlier than 1900-01-01.");
            }

            if (orderDate > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(orderDate), "Future Order Date is not allowed.");
            }
            _orderDate = orderDate.Value;
        }

        public void SetStatus(OrderStatus status)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status))
            {
                throw new ArgumentOutOfRangeException(nameof(status), "Provided Order Status value is not valid.");
            }
            _status = status;
        }

        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product was not provided.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity to add cannot be null or negative.");
            }
            if (_products.ContainsKey(product))
            {
                _products[product] += quantity;
            }
            else
            {
                _products.Add(product, quantity);
            }
        }

        public void RemoveProduct(Product product, int? quantity = null)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product was not provided.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity to remove cannot be null or negative.");
            }
            if (!_products.ContainsKey(product))
            {
                throw new KeyNotFoundException("Product to remove was not found in the Order.");
            }

            if (quantity == null || quantity >= _products[product])
                _products.Remove(product);
            else
                _products[product] -= quantity.Value;
        }
    }
}