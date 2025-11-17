using Catalog.API.CQRS;
using Marten;
using Marten.Pagination;


namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber =1,int? PageSize=10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Models.Products> Products);
    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Models.Products>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10,cancellationToken);
            return new  GetProductsResult(products);
        }
    }

}
