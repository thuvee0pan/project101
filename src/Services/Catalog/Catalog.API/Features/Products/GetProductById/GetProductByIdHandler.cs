
namespace Catalog.API.Features.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(Product Product);

internal class GetProductByIdQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByIdQueryHandler> logger)
    : IRequestHandler<GetProductByIdQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);
        
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByCategoryResult(product);
    }
}