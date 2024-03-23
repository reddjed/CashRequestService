using CashRequestApi.Core.Requests.Commands.CreateRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRequestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestsController(IMediator mediator) =>
        (_mediator) = (mediator);

        [HttpPost]
        public async Task Create([FromBody] CreateRequestCommand request)
        {

            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

            request.ClientIpAddress = remoteIpAddress != null && remoteIpAddress.ToString().Equals("::1") ? "127.0.0.1" : remoteIpAddress?.ToString();

            await _mediator.Send(request);

            return;
        }
    }
}
