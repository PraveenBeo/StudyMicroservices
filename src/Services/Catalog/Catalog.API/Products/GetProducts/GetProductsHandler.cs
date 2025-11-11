using Catalog.API.CQRS;
using Marten;


namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Models.Products> Products);
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsQuery");
            var products = await session.Query<Models.Products>().ToListAsync(cancellationToken);
            return new  GetProductsResult(products);
        }
    }

}
