namespace Catalog.API.Features.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello from Carter!");

        app.MapPost("/products", 
            async (CreateProductRequest request, ISender sender ) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            
            var result = await sender.Send(command);
            
            var response = result.Adapt<CreateProductResponse>();
            
            return Results.Created($"/products/{response.Id}", response);
        }).WithName("CreateProduct")
          .Produces(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Create product")
          .WithDescription("Create product");
        
    }
}