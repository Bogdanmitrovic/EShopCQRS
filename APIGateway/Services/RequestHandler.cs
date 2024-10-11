using APIGateway.Models;

namespace APIGateway.Services;

public class RequestHandler ()
{
    
    public void RegisterRoutes(WebApplication app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id) => { });
        app.MapGet("/products", async () => { });
        app.MapPut("/products", async (Product product) => { });
        app.MapPost("/products", async (Product product) => { });
        app.MapDelete("/products/{id:guid}", async (Guid id) => { });
    }
}