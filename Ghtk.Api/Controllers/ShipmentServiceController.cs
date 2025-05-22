using Ghtk.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ghtk.Api.Controllers;

[Route("/services/shipment")]
[ApiController]
public class ShipmentServiceController : ControllerBase
{
    public  ShipmentServiceController()
    {
    }
    
    [HttpPost]
    [Route("order")]
    [Authorize]
    public IActionResult CreateOrder([FromBody] CreateOrder shipmentOrder)
    {
        return Ok();
    }
}