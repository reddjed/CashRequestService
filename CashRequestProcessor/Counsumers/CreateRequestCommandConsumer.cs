using CashRequestProcessor.Interfaces;
using CashRequestShared.Dto;
using CashRequestShared.Interfaces;
using MassTransit;

namespace CashRequestProcessor.Consumers
{
    public class CreateRequestCommandConsumer : IConsumer<ICreateRequestCommand>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ILogger<CreateRequestCommandConsumer> _logger;

        public CreateRequestCommandConsumer(IRequestRepository requestRepository, ILogger<CreateRequestCommandConsumer> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICreateRequestCommand> context)
        {
            var command = context.Message;
            _logger.LogInformation("Consumed: {@Message}",context.Message);

            var insertedRequestId = await _requestRepository.CreateRequest(command);

            await context.RespondAsync(new InsertedResponseDto
            {
                RequestId = insertedRequestId
            });
        }
    }
}
