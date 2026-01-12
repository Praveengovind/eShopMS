
namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isDeleted);

    internal class DeleteProductCommandHandler(IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product =  await session.LoadAsync<Product>(request.id, cancellationToken);

            if (product is null)
            {
                return new DeleteProductResult(false);
            }

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
