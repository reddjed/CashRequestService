using CashRequestShared.Dto;
using CashRequestShared.Interfaces;
using MediatR;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusById
{
    public class GetRequestStatusByIdQuery : IRequest<RequestStatusDto>, IGetRequestStatusByIdQuery
    {
        public Guid RequestId { get; set; }
    }
}
