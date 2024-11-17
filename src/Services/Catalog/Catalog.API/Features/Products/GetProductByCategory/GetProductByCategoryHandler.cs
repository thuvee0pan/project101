
namespace Catalog.API.Features.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>()
            .Where(p => p.Categories.Contains(query.Category)).ToListAsync(cancellationToken);
        
        return new GetProductByCategoryResult(result);
    }
}