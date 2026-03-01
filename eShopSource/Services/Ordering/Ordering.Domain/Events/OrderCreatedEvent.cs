using System;

namespace Ordering.Domain.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public Order order { get; init; }

    public OrderCreatedEvent(Order order)
    {
        this.order = order ?? throw new ArgumentNullException(nameof(order));
    }
}