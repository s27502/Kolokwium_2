namespace WebApplication1.Models;

public class Payment
{
    public int IdPayment { get; set; }
    public DateTime Date { get; set; }
    public int IdClient { get; set; }
    public Subscription Subscription { get; set; }
    public int amount { get; set;}
    public Client client { get; set; }
    public int IdSubscription { get; set; }
}