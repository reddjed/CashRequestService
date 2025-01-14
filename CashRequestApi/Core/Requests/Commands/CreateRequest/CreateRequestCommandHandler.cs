﻿using CashRequestApi.Options;
using CashRequestApi.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace CashRequestApi.Core.Requests.Commands.CreateRequest
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, Guid>
    {
        private readonly ILogger<CreateRequestCommandHandler> _logger;
        private readonly RabbitMQService _rabbitMqService;
        private readonly RabbitQueuesConfig _rabbitQueues;

        public CreateRequestCommandHandler(
            ILogger<CreateRequestCommandHandler> logger,
            RabbitMQService rabbitMqService,
            IOptionsMonitor<RabbitQueuesConfig> rabbitQueues)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _rabbitQueues = rabbitQueues.CurrentValue;
        }

        public async Task<Guid> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // send by RabbitMQ
                var res = await _rabbitMqService.SendMessageAsync(_rabbitQueues.CreateRequestCommandQueue, request);

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
