using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest();
    public record GetProductsResponse(IEnumerable<Models.Products> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var request = new GetProductsRequest();
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Gets all products")
                .WithDescription("Retrieves a list of all products in the catalog.")
                .WithTags("Products");
        }
    }
}
