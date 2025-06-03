
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
        : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool Deleted);
    public class DeleteProductCommandHandler(IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await session.LoadAsync<Product>(request.Id);

                if (product is null)
                    throw new ProductNotFoundException();

                session.Delete(product);
                await session.SaveChangesAsync(cancellationToken);

                return new DeleteProductResult(true);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
