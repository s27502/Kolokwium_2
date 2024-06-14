namespace WebApplication1.DTOs;

public class ClientDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public IEnumerable<SubscriptionDTO> subscriptions { get; set; }
}