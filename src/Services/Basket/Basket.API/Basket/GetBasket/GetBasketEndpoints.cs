﻿using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket
{
    //public record GetBasketRequest(string Username);
    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
                .WithName("GetBasket")
                .WithSummary("Get a user's shopping cart by username")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get a user's shopping cart by username")
                .WithDescription("Basket")
                ;
        }
    }
}
