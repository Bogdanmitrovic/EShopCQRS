using System.Text;
using APIGateway.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace APIGateway.Services;

public class RabbitMQCommunicator
{
    private static RabbitMQCommunicator? _instance;
    private IConnection _connection;
    private IModel _channel;

    private RabbitMQCommunicator()
    {
        var factory = new ConnectionFactory
        {
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/",
            HostName = "localhost"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "command_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _channel.QueueDeclare(queue: "query_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _instance = this;
    }

    public static RabbitMQCommunicator GetInstance()
    {
        return _instance ??= new RabbitMQCommunicator();
    }

    public void SendMessage(Request request)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        var routingKey = request.RequestType switch
        {
            "GetProizvod" or "ListProizvod" => "query_queue",
            _ => "command_queue"
        };
        _channel.BasicPublish(exchange: string.Empty,
            routingKey: routingKey,
            basicProperties: properties,
            body: body);
        Console.WriteLine($" [x] Sent {body}");
    }
}