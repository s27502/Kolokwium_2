using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IClientService
{
    Task<ClientDTO> GetClientWithSubscriptionsAsync(int clientId);
}
