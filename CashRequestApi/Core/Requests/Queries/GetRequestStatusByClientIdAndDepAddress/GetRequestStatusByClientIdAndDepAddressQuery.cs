using CashRequestShared.Dto;
using CashRequestShared.Interfaces;
using MediatR;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusByClientIdAndDepAddress
{
    public class GetRequestStatusByClientIdAndDepAddressQuery : IRequest<RequestStatusDto>, IGetRequestStatusByClientIdAndDepAddressQuery
    {
        public Guid ClientId { get; set; }
        public string DepartmentAddress { get; set; }
    }
}
