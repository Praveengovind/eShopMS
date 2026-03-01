namespace Ordering.Domain.Enums;

public enum OrderStatus
{
    Draft = 0,
    Pending = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Completed = 6,
    Returned = 7,
    Failed = 8
}
