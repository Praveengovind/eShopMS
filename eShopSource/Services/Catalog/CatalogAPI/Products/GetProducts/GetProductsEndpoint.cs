
using CatalogAPI.Models;
using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.GetProducts
{
    public record GetProductsRequest();

    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products",
               async (ISender sender) =>
               {
                   var result = await sender.Send(new GetProductsQuery());
                   var response = result.Adapt<GetProductsResponse>();

                   return Results.Ok(response);
               })
           .WithName("GetProduct")
           .Produces<CreateProductResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Fetches list of products")
           .WithDescription("Fetches list of products from the catalog.");
        }
    }
}
