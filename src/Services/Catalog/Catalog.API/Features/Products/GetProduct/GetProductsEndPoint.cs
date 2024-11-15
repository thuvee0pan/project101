namespace Catalog.API.Features.Products.GetProduct;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());

            var response = result.Adapt<GetProductResponse>();
            
            return Results.Ok(response);
        })
            .WithName("GetProducts")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products")
            .WithDescription("Get products");
    }
}