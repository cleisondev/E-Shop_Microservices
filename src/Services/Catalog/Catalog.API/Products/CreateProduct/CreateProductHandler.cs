using BuildingBlocks.CQRS;
using Catalog.API.Models;
using System.Windows.Input;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand
        (string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(n => n.Category).NotEmpty().WithMessage("Product category is required.");
            RuleFor(n => n.ImageFile).NotEmpty().WithMessage("Product image file is required.");
            RuleFor(n => n.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        }
    }
    internal class CreateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {

                var product = new Product
                {
                    Name = command.Name,
                    Category = command.Category,
                    Description = command.Description,
                    ImageFile = command.ImageFile,
                    Price = command.Price,
                };

                //TODO
                //save database
                session.Store(product);
                await session.SaveChangesAsync(cancellationToken);

                return new CreateProductResult(product.Id);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    };

}
