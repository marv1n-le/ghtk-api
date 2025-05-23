using Ghtk.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ghtk.Api.Controllers;

[Route("/services/shipment")]
[ApiController]
public class ShipmentServiceController : ControllerBase
{
    private readonly ILogger<ShipmentServiceController> _logger;
    public  ShipmentServiceController(ILogger<ShipmentServiceController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    [Route("order")]
    [Authorize]
    public IActionResult SubmitOrder([FromBody] SubmitOrderRequest order)
    {
        var response = new SubmitOrderResponse()
        {
            Success = true,
            Message = "Order submitted successfully",
            Order = new CreateOrderResponse()
            {
                PartnerId = "partner_id",
                Label = "label",
                Area = 1,
                Fee = 1000,
                InsuranceFee = 0,
                TrackingId = 1,
                EstimatedPickTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                EstimatedDeliverTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Products = [],
                StatusId = 1
            }
        };
        return Ok(response);
    }

    [HttpGet]
    [Route("v2/{id}")]
    [Authorize]
    public IActionResult GetOrderStatus(string id)
    {
        _logger.LogInformation("Getting order status for id: {id}", id);
        var response = new GetOrderStatusResponse()
        {
            Success = true,
            Message = "Order status retrieved successfully",
            Order = new Order()
            {
                LabelId = id,
                PartnerId = "partner_id",
                Status = "status",
                StatusText = "status_text",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Message = "message",
                PickDate = DateTime.UtcNow,
                DeliverDate = DateTime.UtcNow,
                CustomerFullname = "customer_fullname",
                CustomerTel = "customer_tel",
                Address = "address",
                StorageDay = 1,
                ShipMoney = 1000,
                Insurance = 0,
                Value = 1000
            }
        };
        return Ok(response);
    }

    [HttpPost]
    [Route("cancel/{id}")]
    public IActionResult CancelOrder(string id)
    {
        _logger.LogInformation("Cancelling order with id: {id}", id);
        var response = new ApiResult()
        {
            Success = true,
            Message = "Order cancelled successfully"
        };
        return Ok();
    }
        
        
    
}