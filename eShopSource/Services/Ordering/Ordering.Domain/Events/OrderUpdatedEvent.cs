namespace Ordering.Domain.Events;

public class OrderUpdatedEvent : IDomainEvent
{
    public Order Order { get; init; }

    public OrderUpdatedEvent(Order order)
    {
        Order = order ?? throw new ArgumentNullException(nameof(order));
    }

}
