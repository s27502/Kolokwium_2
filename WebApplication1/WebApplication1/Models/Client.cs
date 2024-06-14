namespace WebApplication1.Models;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public IEnumerable<Payment> payments { get; set; }
    public IEnumerable<Subscription> Subscriptions { get; set; }
}
