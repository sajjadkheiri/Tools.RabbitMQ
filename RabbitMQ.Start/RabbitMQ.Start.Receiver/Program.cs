using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

var connection = await factory.CreateConnectionAsync();
var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync("hello", false, false, false);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += ConsumerOnReceivedAsync;

await channel.BasicConsumeAsync("hello", false, consumer);

Console.WriteLine("Press [Enter] to exit...");
Console.ReadLine();

Task ConsumerOnReceivedAsync(object sender, BasicDeliverEventArgs @event)
{
    var body = @event.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    
    Console.WriteLine($" [+] Message is \"{message}\"");
    
    return Task.CompletedTask;
}