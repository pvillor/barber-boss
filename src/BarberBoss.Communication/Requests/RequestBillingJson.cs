using CashFlow.Communication.Enums;

namespace BarberBoss.Communication.Requests;

public class RequestBillingJson
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
}
