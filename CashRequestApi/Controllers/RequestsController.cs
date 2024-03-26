using CashRequestApi.Core.Requests.Commands.CreateRequest;
using CashRequestApi.Core.Requests.Queries.GetRequestStatusByClientIdAndDepAddress;
using CashRequestApi.Core.Requests.Queries.GetRequestStatusById;
using CashRequestShared.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRequestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RequestsController> _logger;

        public RequestsController(IMediator mediator, ILogger<RequestsController> logger) =>
        (_mediator, _logger) = (mediator, logger);

        [HttpGet("{RequestId}")]
        public async Task<RequestStatusDto> GetRequestById([FromRoute] GetRequestStatusByIdQuery request)
        {
            // Client ip
            _logger.LogInformation($"Client IP: {HttpContext.Connection.RemoteIpAddress?.ToString()}");

            var res = await _mediator.Send(request);

            return res;
        }
        [HttpPost]
        public async Task<Guid> Create([FromBody] CreateRequestCommand request)
        {
            // Client ip
            _logger.LogInformation($"Client IP: {HttpContext.Connection.RemoteIpAddress?.ToString()}"); 

            var res = await _mediator.Send(request);

            return res;
        }
        [HttpPost]
        [Route("GetRequestByClientIdAndDepAdress")]
        public async Task<RequestStatusDto> GetRequestByClientIdAndDepAdress([FromBody] GetRequestStatusByClientIdAndDepAddressQuery request)
        {
            // Client ip
            _logger.LogInformation($"Client IP: {HttpContext.Connection.RemoteIpAddress?.ToString()}");

            var res = await _mediator.Send(request);

            return res;
        }

    }
}
