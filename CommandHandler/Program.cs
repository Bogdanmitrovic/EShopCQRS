using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var factory = new ConnectionFactory
{
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/",
    HostName = "localhost"
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare(queue: "command_queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (ch, ea) =>
{
    Console.WriteLine("received!");
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    
    Console.WriteLine(message);
    channel.BasicAck(ea.DeliveryTag, false);
};

var consumerTag = channel.BasicConsume("command_queue", false, consumer);


app.Run();

channel.BasicCancel(consumerTag);