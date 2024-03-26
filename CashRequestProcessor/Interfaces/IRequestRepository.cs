using CashRequestShared.Dto;
using CashRequestShared.Interfaces;

namespace CashRequestProcessor.Interfaces
{
    public interface IRequestRepository
    {
        Task<Guid> CreateRequest(ICreateRequestCommand request);
        Task<RequestStatusDto> GetRequestStatusById(IGetRequestStatusByIdQuery query);
        Task<RequestStatusDto> GetRequestStatusByClientIdAndAddress(IGetRequestStatusByClientIdAndDepAddressQuery query);
    }
}
