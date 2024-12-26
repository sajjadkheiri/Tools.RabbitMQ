using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};

var connection = await factory.CreateConnectionAsync();
var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync("hello", false, false, false);

Console.WriteLine(" [*] Type your messages : ");

var message = Console.ReadLine() ?? "Default message";
var body = Encoding.UTF8.GetBytes(message).ToArray();

await channel.BasicPublishAsync("", "hello", body);

Console.WriteLine(" [x] Sent {0}", message);

await channel.CloseAsync();