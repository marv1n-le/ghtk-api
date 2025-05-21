using Ghtk.Api.Models;
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
    public IActionResult CreateOrder([FromBody] CreateOrder shipmentOrder)
    {
        return Ok();
    }
}