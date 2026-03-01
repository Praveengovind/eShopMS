namespace Ordering.Domain.ValueObjects;

public record Payment(string CardNumber, string CardHolderName, DateTime ExpirationDate, string CVV, int PaymentMethod)
{
    public static Payment Of(string cardNumber, string cardHolderName, DateTime expirationDate, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardHolderName, nameof(cardHolderName));
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        if (expirationDate < DateTime.UtcNow)
        {
            throw new ArgumentException("Expiration date cannot be in the past.", nameof(expirationDate));
        }

        return new Payment(cardNumber, cardHolderName, expirationDate, cvv, paymentMethod);
    }
}
