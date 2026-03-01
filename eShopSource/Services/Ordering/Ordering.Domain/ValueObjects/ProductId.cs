namespace Ordering.Domain.ValueObjects;

public record ProductId(Guid Value)
{
    public Guid Value { get; }

    public static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("ProductId cannot be empty.", nameof(value));
        }

        return new ProductId(value);
    }
}
