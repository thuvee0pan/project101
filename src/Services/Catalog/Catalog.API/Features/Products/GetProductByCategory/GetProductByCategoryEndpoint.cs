namespace Catalog.API.Features.Products.GetProductByCategory;

// public record GetProductByCategoryRequest(Guid Id);
public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}", async ( string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();
            
            return Results.Ok(response);
        })
            .WithName("GetProductByCategory")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product by Category")
            .WithDescription("Get product by Category");
    }
}