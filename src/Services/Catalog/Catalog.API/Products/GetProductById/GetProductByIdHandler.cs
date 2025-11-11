using Catalog.API.CQRS;
using Catalog.API.Exceptions;
using Marten;


namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Models.Products Product);

    internal class GetProductByIdQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Models.Products>(query.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException("Product", query.Id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
