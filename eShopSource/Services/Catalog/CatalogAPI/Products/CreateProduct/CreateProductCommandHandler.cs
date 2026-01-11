using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price): ICommand<CreateProductResult>;
    public record CreateProductResult(
        Guid Id);

    internal class CreateProductCommandHandler (IDocumentSession documentSession)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Business logic goes here...

            //create a new product entity from command object
            var product = new Product
            {

                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //save product to database (omitted for brevity) - TODO: Implement database save logic here
            documentSession.Store(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            //return the result with the new product ID
            return new CreateProductResult(product.Id);
        }
    }
}
