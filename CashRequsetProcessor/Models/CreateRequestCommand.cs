namespace CashRequsetProcessor.Models;

public class CreateRequestCommand
{
    public Guid ClientId { get; set; }
    public string? DepartmentAddress { get; set; } 
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public string? ClientIpAddress { get; set; }
}
