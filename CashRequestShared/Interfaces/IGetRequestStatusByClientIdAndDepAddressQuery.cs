namespace CashRequestShared.Interfaces
{
    public interface IGetRequestStatusByClientIdAndDepAddressQuery
    {
        public Guid ClientId { get; set; }
        public string DepartmentAddress { get; set; }
    }
}
