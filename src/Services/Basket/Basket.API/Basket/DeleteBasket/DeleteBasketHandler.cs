
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(string Username);

    public class DeleteBasketHandler(IDocumentSession session)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var cart = session.Load<ShoppingCart>(request.Username);    
            if(cart is null)
                throw new Exception($"Basket with username {request.Username} not found.");

            session.Delete(cart);

            return session.SaveChangesAsync(cancellationToken)
                .ContinueWith(_ => new DeleteBasketResult(request.Username), cancellationToken);
        }
    }
}
