using CashRequestProcessor.Consumers;
using CashRequestProcessor.Interfaces;
using CashRequestShared.Interfaces;
using MassTransit;

namespace CashRequestProcessor.Counsumers
{
    public class GetRequestStatusByClientIdAndDepAddressQueryConsumer : IConsumer<IGetRequestStatusByClientIdAndDepAddressQuery>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ILogger<CreateRequestCommandConsumer> _logger;

        public GetRequestStatusByClientIdAndDepAddressQueryConsumer(IRequestRepository requestRepository, ILogger<CreateRequestCommandConsumer> logger)
        {
            _requestRepository = requestRepository;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGetRequestStatusByClientIdAndDepAddressQuery> context)
        {
            var qeury = context.Message;
            _logger.LogInformation("Consumed: {@Message}", context.Message);

            var res = await _requestRepository.GetRequestStatusByClientIdAndAddress(qeury);

            await context.RespondAsync(res);
        }
    }
}
