
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price);

    public record UpdateProductResponse(bool IsUpdated);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id}",
                async (Guid id, UpdateProductRequest request, ISender sender) =>
            {
                if (id != request.Id)
                {
                    return Results.BadRequest("ID in URL does not match ID in request body.");
                }
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = new UpdateProductResponse(result.IsUpdated);
                return Results.Ok(response);
            });
        }
    }
}
