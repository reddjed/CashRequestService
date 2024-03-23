using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace CashRequestApi.Services
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory _rabbitMqFactory;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(ConnectionFactory rabbitMqFactory, ILogger<RabbitMQService> logger)
        {
            _rabbitMqFactory = rabbitMqFactory;
            _logger = logger;
        }

        public async Task<string> SendMessageAsync<T>(string queueName, T message)
        {
            try
            {
                using (var connection = _rabbitMqFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    // Generate a unique correlation id for this request
                    string correlationId = Guid.NewGuid().ToString();

                    // Declare the queue
                    channel.QueueDeclare(queue: queueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string serializedMessage = JsonConvert.SerializeObject(message);

                    _logger.LogInformation("Request to create an application was received: {@serializedMessage}", serializedMessage);

                    var body = Encoding.UTF8.GetBytes(serializedMessage);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.CorrelationId = correlationId;

                    var tcs = new TaskCompletionSource<string>();

                    // Set up the event handler to handle responses from RabbitMQ
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        if (ea.BasicProperties.CorrelationId == correlationId)
                        {
                            var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                            tcs.SetResult(response);
                        }
                    };

                    // Subscribe to the queue to receive the response
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    // Publish the message to RabbitMQ
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);

                    // Log the message sent to RabbitMQ
                    _logger.LogInformation($"Message sent to RabbitMQ queue '{queueName}' with correlation id '{correlationId}'");

                    // Await the response from RabbitMQ
                    string response = await tcs.Task;

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during message sending
                _logger.LogError(ex, $"Error sending message to RabbitMQ queue '{queueName}'");
                throw;
            }
        }
    }
}

