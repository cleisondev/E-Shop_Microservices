namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketHandler(IDocumentSession session)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var cart = await session.LoadAsync<ShoppingCart>(request.Username, cancellationToken);

            return new GetBasketResult(cart ?? new ShoppingCart
            {
                Username = request.Username,
                Items = new List<ShoppingCartItem>()
            });
        }
    }
}
