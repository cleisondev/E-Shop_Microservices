
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand
        (Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool updated);

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(n => n.Id).NotEmpty().WithMessage("Product Id cannot be null");
            RuleFor(n => n.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(n => n.Category).NotEmpty().WithMessage("Product category is required.");
            RuleFor(n => n.ImageFile).NotEmpty().WithMessage("Product image file is required.");
            RuleFor(n => n.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {

                var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

                if(product is null)
                    throw new ProductNotFoundException(command.Id);

                product.Name = command.Name;
                product.Category = command.Category;
                product.Description = command.Description;
                product.ImageFile = command.ImageFile;
                product.Price = command.Price;


                session.Update(product);
                await session.SaveChangesAsync(cancellationToken);

                return new UpdateProductResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
