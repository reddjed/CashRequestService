using CashRequestApi.Services;
using MediatR;
using System.Net;
using System.Net.NetworkInformation;

namespace CashRequestApi.Core.Requests.Commands.CreateRequest
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand>
    {
        private readonly ILogger<CreateRequestCommandHandler> _logger;
        private readonly RabbitMQService _rabbitMqService;

        public CreateRequestCommandHandler(ILogger<CreateRequestCommandHandler> logger, RabbitMQService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public async Task Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Client ip
                _logger.LogInformation($"Client IP: {request.ClientIpAddress}");

                // send by RabbitMQ
                var res = await _rabbitMqService.SendMessageAsync("save_cash_request_queue", request);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing a request to create a request");
                throw;
            }
        }
    }
}
