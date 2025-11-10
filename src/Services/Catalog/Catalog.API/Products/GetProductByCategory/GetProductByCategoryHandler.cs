
using Marten;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IRequest<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Models.Products> Products);

internal class GetProductByCategoryQueryHandler
    (IDocumentSession session)
    : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Models.Products>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
