using CashRequestShared.Enums;

namespace CashRequestShared.Interfaces
{
    public interface ICreateRequestCommand
    {
        public Guid ClientId { get; set; }
        public string DepartmentAddress { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public RequestStatus Status { get; set; }
    }
}
