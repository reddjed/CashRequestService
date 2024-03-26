using CashRequestProcessor.Consumers;
using CashRequestProcessor.Interfaces;
using CashRequestShared.Interfaces;
using MassTransit;

namespace CashRequestProcessor.Counsumers
{
    public class GetRequestStatusByIdQueryConsumer : IConsumer<IGetRequestStatusByIdQuery>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ILogger<CreateRequestCommandConsumer> _logger;

        public GetRequestStatusByIdQueryConsumer(IRequestRepository requestRepository, ILogger<CreateRequestCommandConsumer> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IGetRequestStatusByIdQuery> context)
        {
            var qeury = context.Message;
            _logger.LogInformation("Consumed: {@Message}", context.Message);

            var res = await _requestRepository.GetRequestStatusById(qeury);

            await context.RespondAsync(res);
        }
    }
}
