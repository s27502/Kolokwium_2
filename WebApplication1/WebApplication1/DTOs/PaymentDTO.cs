namespace WebApplication1.DTOs;

public class PaymentDto
{
    public int ClientId { get; set; }
    public int SubscriptionId { get; set; }
    public decimal Amount { get; set; }
}