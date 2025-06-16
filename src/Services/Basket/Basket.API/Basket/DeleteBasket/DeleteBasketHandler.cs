
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool isSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");
        }
    }   

    public class DeleteBasketHandler(IDocumentSession session)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var cart = session.Load<ShoppingCart>(request.Username);    
            if(cart is null)
                throw new Exception($"Basket with username {request.Username} not found.");

            session.Delete(cart);

            return new DeleteBasketResult(true);
        }
    }
}
