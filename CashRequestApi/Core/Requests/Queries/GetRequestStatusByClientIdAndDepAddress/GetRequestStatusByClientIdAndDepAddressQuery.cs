using MediatR;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusByClientIdAndDepAddress
{
    public class GetRequestStatusByClientIdAndDepAddressQuery : IRequest
    {
        public Guid ClientId { get; set; }
        public string DepartmentAddress { get; set; }
    }
}
