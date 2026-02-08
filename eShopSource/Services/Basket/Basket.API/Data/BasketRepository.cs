
namespace Basket.API.Data;

public class BasketRepository(IDocumentSession documentSession) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await documentSession.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return basket is null ? throw new BasketNotFoundException($"Basket with user name '{userName}' not found.") 
            : basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        documentSession.Store(shoppingCart);
        await documentSession.SaveChangesAsync(cancellationToken);
        return shoppingCart;
    }
    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        documentSession.Delete<ShoppingCart>(userName);
        await documentSession.SaveChangesAsync(cancellationToken);
        return true;
    }
}
