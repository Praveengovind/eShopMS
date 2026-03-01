namespace Ordering.Domain.ValueObjects
{
    public record OrderName(string Value)
    {
        private const int DefaultLength = 100;
        public string Value { get; }

        private static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, DefaultLength, nameof(value));

            if (value.Length > DefaultLength)
            {
                throw new ArgumentException($"Order name cannot exceed {DefaultLength} characters.", nameof(value));
            }

            return new OrderName(value);
        }
    }
}
