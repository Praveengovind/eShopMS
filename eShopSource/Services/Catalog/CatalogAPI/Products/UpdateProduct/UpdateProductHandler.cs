
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id, 
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);
    internal class UpdateProductHandler(IDocumentSession session)
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
