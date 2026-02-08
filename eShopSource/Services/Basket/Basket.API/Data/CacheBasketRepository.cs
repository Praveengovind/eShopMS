namespace Basket.API.Data;

public class CacheBasketRepository(IBasketRepository basketRepository,
    IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
        }

        var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize<ShoppingCart>(basket));

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasketAsync(shoppingCart, cancellationToken);

        await cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize<ShoppingCart>(shoppingCart), cancellationToken);
        return shoppingCart;
    }
    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);

        // Check if a cache entry exists first
        var cached = await cache.GetStringAsync(userName, cancellationToken);
        if (string.IsNullOrEmpty(cached))
        {
            // Nothing to remove from cache
            return true;
        }

        await cache.RemoveAsync(userName, cancellationToken);
        return true;
    }
}
