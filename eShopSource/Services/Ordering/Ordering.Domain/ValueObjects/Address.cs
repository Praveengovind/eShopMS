namespace Ordering.Domain.ValueObjects;

public record Address(string? EmailAddress,string AddressLine, string City, string State, string ZipCode)
{
   
    public static Address Of(string? emailAddress, string addressLine, string city, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine, nameof(addressLine));
        ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
        ArgumentException.ThrowIfNullOrWhiteSpace(state, nameof(state));
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode, nameof(zipCode));

        return new Address(emailAddress, addressLine, city, state, zipCode);
    }
}
