using CashRequestApi.Core.Requests.Commands.CreateRequest;
using MassTransit;
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
            request.ClientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            await _mediator.Send(request);

            return;
        }
    }
}
