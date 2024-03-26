using CashRequestApi.Options;
using CashRequestApi.Services;
using CashRequestShared.Dto;
using MediatR;
using Microsoft.Extensions.Options;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusById
{
    public class GetRequestStatusByIdQueryHandler : IRequestHandler<GetRequestStatusByIdQuery, RequestStatusDto>
    {
        private readonly ILogger<GetRequestStatusByIdQueryHandler> _logger;
        private readonly RabbitMQService _rabbitMqService;
        private readonly RabbitQueuesConfig _rabbitQueues;

        public GetRequestStatusByIdQueryHandler(
            ILogger<GetRequestStatusByIdQueryHandler> logger,
            RabbitMQService rabbitMqService,
            IOptionsMonitor<RabbitQueuesConfig> rabbitQueues)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _rabbitQueues = rabbitQueues.CurrentValue;
        }
        public async Task<RequestStatusDto> Handle(GetRequestStatusByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // send by RabbitMQ
                var res = await _rabbitMqService.SendRequestByIdQueryAsync(_rabbitQueues.GetRequestStatusByIdQueue, request);

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
