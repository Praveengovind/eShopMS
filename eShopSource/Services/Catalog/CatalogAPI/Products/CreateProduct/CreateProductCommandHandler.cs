using FluentValidation;

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

    internal class CreateProductCommandHandler 
        (IDocumentSession documentSession)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //validate the command - manual validation approach - Removed
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);
            //var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

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
