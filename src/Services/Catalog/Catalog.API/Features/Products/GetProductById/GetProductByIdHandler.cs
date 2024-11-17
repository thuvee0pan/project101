
namespace Catalog.API.Features.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession session)
    : IRequestHandler<GetProductByIdQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByCategoryResult(product);
    }
}