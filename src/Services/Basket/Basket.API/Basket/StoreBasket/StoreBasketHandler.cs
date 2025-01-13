namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull()
                .WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName)
            .NotNull()
            .NotEmpty()
                .WithMessage("Username is required");
    }
}
public class StoreBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(basket.UserName);
    }
}