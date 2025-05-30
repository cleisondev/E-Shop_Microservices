
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest();
    public record GetProductsResponse(IEnumerable<Product> products);
    public class GetProductsEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
             app.MapGet("/products", async (ISender sender) =>
            {

                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProduct")
                .Produces<GetProductsResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product")
                .WithDescription("Get Product");
        }
    }
}
