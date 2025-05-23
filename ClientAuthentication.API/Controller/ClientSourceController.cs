using Microsoft.AspNetCore.Mvc;

namespace ClientAuthentication.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ClientSourceController : ControllerBase
{
    private readonly ILogger<ClientSourceController> _logger;
    private readonly IClientSourceAuthenticationHandler _handler;
    public ClientSourceController(IClientSourceAuthenticationHandler handler, ILogger<ClientSourceController> logger)
    {
        _handler = handler;
        _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        _logger.LogInformation("Authenticating client id: {id}", id);

        if (_handler.Validate(id))
        {
            _logger.LogInformation("Client id {id} is valid", id);
            return Ok();
        }

        _logger.LogInformation("Client id {id} is invalid", id);
        return Unauthorized();
    }
}