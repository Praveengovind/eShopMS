namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResponse(bool isDeleted);

    public class DeleteProductEndpoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",
                async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                if (!result.isDeleted)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new { Deleted = result.isDeleted });
            })
            .WithName("DeleteProduct")
            .WithTags("Products")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product")
            .WithDescription("Deletes a product from the catalog by its ID.");
        }
    }
}
