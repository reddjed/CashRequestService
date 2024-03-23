using MassTransit;
using MassTransit.Transports;
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
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public RabbitMQService(
            ConnectionFactory rabbitMqFactory,
            ILogger<RabbitMQService> logger,
            ISendEndpointProvider sendEndpointProvider)
        {
            _rabbitMqFactory = rabbitMqFactory;
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<string> SendMessageAsync<T>(string queueName, T message)
        {
            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(queueName));
                
                await endpoint.Send(message, context =>
                {
                    context.CorrelationId = Guid.NewGuid();
                });

                return string.Empty;
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

