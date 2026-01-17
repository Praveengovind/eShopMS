
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id, 
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);

    public class UpdateProductCommandValidator
        : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.id);
            if (product is null)
            {
                throw new ProductNotFoundException(request.id);
            }
            product.Name = request.Name;
            product.Category = request.Category;                
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(IsUpdated: true);
        }
    }
}
