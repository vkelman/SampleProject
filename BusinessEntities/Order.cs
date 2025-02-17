using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Represents an Order entity.
    /// </summary>
    public class Order : IdObject
    {
        private DateTime _orderDate = DateTime.MinValue;
        private OrderStatus _status = OrderStatus.Undefined;
        private Guid _userId;
        private Dictionary<Guid, int> _products = new Dictionary<Guid, int>();
        private IList<(OrderStatus status, Dictionary<Guid, int> products)> _orderHistory = new List<(OrderStatus status, Dictionary<Guid, int> products)>();

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public OrderStatus Status
        {
            get => _status;
            private set => _status = value;
        }

        public Guid UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public Dictionary<Guid, int> Products
        {
            get => _products;
            private set => _products = value;
        }

        public IList<(OrderStatus status, Dictionary<Guid, int> products)> OrderHistory
        {
            get => _orderHistory;
            private set => _orderHistory = value;
        }

        public void SetOrderDate(DateTime? orderDate)
        {
            if (orderDate == null)
            {
                throw new ArgumentNullException(nameof(orderDate), "Order Date was not provided.");
            }
            if (orderDate < Constants.BusinessConstants.EarliestOrderDate)
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
            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] += quantity;
            }
            else
            {
                _products.Add(product.Id, quantity);
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
            if (!_products.ContainsKey(product.Id))
            {
                throw new KeyNotFoundException("Product to remove was not found in the Order.");
            }

            if (quantity == null || quantity >= _products[product.Id])
                _products.Remove(product.Id);
            else
                _products[product.Id] -= quantity.Value;
        }

        public void SetProducts(Dictionary<Guid, int> products)
        {
            _products.Clear();

            if (products == null)
            {
                throw new ArgumentNullException(nameof(products), "Products were not provided.");
            }
            if (products.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(products), "Products to set cannot be empty.");
            }
            _products = products;
        }

        public IList<(OrderStatus status, Dictionary<Guid, int> products)> AddToOrderHistory(
            IList<(OrderStatus status, Dictionary<Guid, int> products)> history,
            Dictionary<Guid, int> products)
        {
            history.Add((Status, products));

            return history;
        }
    }
}