using APIGateway.Models;
namespace APIGateway.Services;

public class RequestHandler ()
{
    
    public static void RegisterRoutes(WebApplication app)
    {
        app.MapGet("/proizvodi/{id:guid}", async (Guid id) =>
        {
            var request = new Request(new Proizvod { Id = id }, "GetProizvod");
            RabbitMQCommunicator.GetInstance().SendMessage(request);
        });

        app.MapGet("/proizvodi", async () =>
        {
            var request = new Request(new Proizvod(), "ListProizvod");
            RabbitMQCommunicator.GetInstance().SendMessage(request);
        });

        app.MapPut("/proizvodi", async (Proizvod proizvod) =>
        {
            var request = new Request(proizvod, "UpdateProizvod");
            RabbitMQCommunicator.GetInstance().SendMessage(request);
        });

        app.MapPost("/proizvodi", async (Proizvod proizvod) =>
        {
            var request = new Request(proizvod, "CreateProizvod");
            RabbitMQCommunicator.GetInstance().SendMessage(request);
        });

        app.MapDelete("/proizvodi/{id:guid}", async (Guid id) =>
        {
            var request = new Request(new Proizvod { Id = id }, "DeleteProizvod");
            RabbitMQCommunicator.GetInstance().SendMessage(request);
        });
    }
}