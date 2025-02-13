using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private string _description;
        private ProductTypes _type = ProductTypes.Grocery;
        private decimal _price;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }
  
        public ProductTypes Type
        {
            get => _type;
            private set => _type = value;
        }
  
        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Product Name was not provided.");
            }
            _name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "Product Description was not provided.");
            }
            _description = description;
        }
  
        public void SetType(ProductTypes type)
        {
            if (!Enum.IsDefined(typeof(ProductTypes), type))
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Provided Product Type value is not valid.");
            }
            _type = type;
        }
 
        public void SetPrice(decimal? price)
        {
            if (price == null)
            {
                throw new ArgumentNullException(nameof(price), "Price was not provided.");
            }
            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be null or negative.");
            }
            _price = price.Value;
        }
    }
}