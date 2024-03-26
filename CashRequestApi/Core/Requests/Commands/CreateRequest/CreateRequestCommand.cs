using CashRequestShared.Enums;
using CashRequestShared.Interfaces;
using MediatR;

namespace CashRequestApi.Core.Requests.Commands.CreateRequest
{
    public class CreateRequestCommand : IRequest<Guid>, ICreateRequestCommand
    {
        public Guid ClientId { get; set; }
        public string DepartmentAddress { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.InProgress;
    }
}
