using MediatR;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusById
{
    public class GetRequestStatusByIdQuery : IRequest
    {
        public Guid RequestId { get; set; }
    }
}
