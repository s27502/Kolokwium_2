
using WebApplication1.DTOs;
using WebApplication1.Services;
namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
    {
    private readonly IClientService _clientService;
    public ClientsController(IClientService clientService)
    {
    _clientService = clientService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDTO>> GetClientWithSubscriptions(int id)
    {
        var clientDto = await _clientService.GetClientWithSubscriptionsAsync(id);
        if (clientDto == null) {
            return NotFound();
        }
        return Ok(clientDto);
    }
}
