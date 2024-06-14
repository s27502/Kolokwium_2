using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ClientDTO> GetClientWithSubscriptionsAsync(int clientId)
    {
        var client = await _context.Clients
            .Include(c => c.Subscriptions)
            .FirstOrDefaultAsync(c => c.Id == clientId);
        if (client == null)
        {
            return null;
        }
        var clientDto = new ClientDTO();
        clientDto.firstName = client.FirstName;
        clientDto.lastName = client.LastName;
        clientDto.email = client.Email;
        clientDto.phone = client.Phone;

        return clientDto;

    }
}
