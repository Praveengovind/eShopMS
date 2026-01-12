
namespace CatalogAPI.Products.GetProductByCatagory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                 .Where(p => p.Category.Contains(query.Category))
                 .ToListAsync();
            var result = new GetProductByCategoryResult(products);
            return result;
        }
    }
}
