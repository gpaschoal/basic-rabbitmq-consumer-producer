using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare("messageQueue", false, false, false, null);

    var objToSerialize = new { MessageToBeSerialized = "This message will be serialized", version = "1.0" };

    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objToSerialize));

    channel.BasicPublish("", "messageQueue", null, body:body);

    Console.WriteLine(" [x] Sent {0}", objToSerialize);
}

// Console.WriteLine(" Press [enter] to exit.");
// Console.ReadLine();