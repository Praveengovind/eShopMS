
namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(bool IsSuccess);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Shopping cart cannot be null.");
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("User name cannot be empty.");
    }
}

public class StoreBasketHandler(IBasketRepository basketRepository, 
    DiscountService.DiscountServiceClient discountService)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.ShoppingCart.Items)
        {
            var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
            var discountResponse = await discountService.GetDiscountAsync(discountRequest, cancellationToken: cancellationToken);
            // Cast double to decimal to match item.Price type (decimal)
            item.Price -= discountResponse.Amount;
        }

        ShoppingCart shoppingCart = command.ShoppingCart;

        await basketRepository.StoreBasketAsync(shoppingCart,cancellationToken);

        return new StoreBasketResult(IsSuccess: true);
    }
}
