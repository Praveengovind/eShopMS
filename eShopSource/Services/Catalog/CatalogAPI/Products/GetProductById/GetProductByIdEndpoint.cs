
namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}",
               async (Guid id, ISender sender) =>
               {
                   var result = await sender.Send(new GetProductByIdQuery(id));
                   var response = result.Adapt<GetProductByIdResponse>();
                   return Results.Ok(response);
               })
                .WithName("GetProductById")
                .WithTags("Products")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get product by Id")
                .WithDescription("Gets a product by its unique identifier.");
        }
    }
}
