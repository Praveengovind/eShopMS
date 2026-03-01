namespace Ordering.Domain.Models;

public class OrderItem: Entity<OrderItemId>
{
    //Leads to primitive obsession, but for simplicity, we will keep it this way.
    //In a real-world application, you might want to create value objects for ProductName and Price.
    //internal OrderItem(Guid orderId, Guid productId, string productName, decimal price, int quantity)
    internal OrderItem(OrderId orderId, ProductId productId,decimal price, int quantity)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
}
