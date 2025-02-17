namespace WebApi.Models.Orders
{
    public class OrderProductsData
    {
        public OrderProductsData(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}