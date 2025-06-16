
namespace Basket.API.Data
{
    public class BaskedRepository(IDocumentSession session)
        : IBaskedRepository
    {


        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }


        public Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            return session.SaveChangesAsync(cancellationToken).ContinueWith(t => true, cancellationToken);
        }
    }
}
