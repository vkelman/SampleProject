namespace BusinessEntities
{
    public enum OrderStatus
    {
        Undefined = 0,
        New = 1,
        Placed = 2,
        Cancelled = 3,
        Refunded = 4,
        Returned = 5,
        Shipped = 6,
        Delivered = 7
    }
}