using CashRequestApi.Core.Requests.Commands.CreateRequest;
using CashRequestApi.Core.Requests.Queries.GetRequestStatusByClientIdAndDepAddress;
using CashRequestApi.Core.Requests.Queries.GetRequestStatusById;
using CashRequestShared.Dto;
using MassTransit;
namespace CashRequestApi.Services
{
    public class RabbitMQService
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRequestClient<CreateRequestCommand> _createRequestClient;
        private readonly IRequestClient<GetRequestStatusByIdQuery> _getRequestStatusByIdClient;
        private readonly IRequestClient<GetRequestStatusByClientIdAndDepAddressQuery> _getRequestStatusByClientIdAndDepAddressClient;
        public RabbitMQService(
            ILogger<RabbitMQService> logger,
            ISendEndpointProvider sendEndpointProvider,
            IRequestClient<CreateRequestCommand> createRequestClient,
            IRequestClient<GetRequestStatusByIdQuery> getRequestStatusByIdClient,
            IRequestClient<GetRequestStatusByClientIdAndDepAddressQuery> getRequestStatusByClientIdAndDepAddressClient)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _createRequestClient = createRequestClient;
            _getRequestStatusByIdClient = getRequestStatusByIdClient;
            _getRequestStatusByClientIdAndDepAddressClient = getRequestStatusByClientIdAndDepAddressClient;
        }

        public async Task<Guid> SendMessageAsync(string queueName, CreateRequestCommand message)
        {
            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(queueName));

                var res = await _createRequestClient.GetResponse<InsertedResponseDto>(message);

                return res.Message.RequestId;
            }
            catch (Exception ex)
            {
                // Log any errors that occur during message sending
                _logger.LogError(ex, $"Error sending message to RabbitMQ queue '{queueName}'");
                throw;
            }
        }
        public async Task<RequestStatusDto> SendRequestByIdQueryAsync(string queueName, GetRequestStatusByIdQuery message)
        {
            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(queueName));

                var res = await _getRequestStatusByIdClient.GetResponse<RequestStatusDto>(message);

                return res.Message;
            }
            catch (Exception ex)
            {
                // Log any errors that occur during message sending
                _logger.LogError(ex, $"Error sending message to RabbitMQ queue '{queueName}'");
                throw;
            }
        }
        public async Task<RequestStatusDto> SendRequestStatusByClientIdAndDepAddressQueryAsync(string queueName, GetRequestStatusByClientIdAndDepAddressQuery message)
        {
            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(queueName));

                var res = await _getRequestStatusByClientIdAndDepAddressClient.GetResponse<RequestStatusDto>(message);

                return res.Message;
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

