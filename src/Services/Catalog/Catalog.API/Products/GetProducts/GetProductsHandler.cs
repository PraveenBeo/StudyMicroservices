using Marten;
using MediatR;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IRequest<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Models.Products> Products);
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsQuery");
            var products = await session.Query<Models.Products>().ToListAsync(cancellationToken);
            return new  GetProductsResult(products);
        }
    }

}
