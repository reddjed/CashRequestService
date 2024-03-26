
using CashRequestApi.Options;
using CashRequestApi.Services;
using CashRequestShared.Dto;
using MediatR;
using Microsoft.Extensions.Options;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusByClientIdAndDepAddress
{
    public class GetRequestStatusByClientIdAndDepAddressQueryHandler : IRequestHandler<GetRequestStatusByClientIdAndDepAddressQuery, RequestStatusDto>
    {
        private readonly ILogger<GetRequestStatusByClientIdAndDepAddressQueryHandler> _logger;
        private readonly RabbitMQService _rabbitMqService;
        private readonly RabbitQueuesConfig _rabbitQueues;

        public GetRequestStatusByClientIdAndDepAddressQueryHandler(
            ILogger<GetRequestStatusByClientIdAndDepAddressQueryHandler> logger,
            RabbitMQService rabbitMqService,
            IOptionsMonitor<RabbitQueuesConfig> rabbitQueues)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _rabbitQueues = rabbitQueues.CurrentValue;
        }
        public async Task<RequestStatusDto> Handle(GetRequestStatusByClientIdAndDepAddressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // send by RabbitMQ
                var res = await _rabbitMqService.SendRequestStatusByClientIdAndDepAddressQueryAsync(_rabbitQueues.GetRequestStatusByClientIdAndDepAddressQueue, request);

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing a request to create an application");
                throw;
            }
        }
    }
}
