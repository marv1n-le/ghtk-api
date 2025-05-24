using Ghtk.Api.Models;
using Ghtk.Repository.Abstractions;
using Ghtk.Repository.Abstractions.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ghtk.Api.Controllers;

[Route("/services/shipment")]
[ApiController]
public class ShipmentServiceController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<ShipmentServiceController> _logger;
    public  ShipmentServiceController(IOrderRepository orderRepository, ILogger<ShipmentServiceController> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    
    [HttpPost]
    [Route("order")]
    [Authorize]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderRequest order)
    {
        _logger.LogInformation("Submitting order: {@order}", order);
        
        var partnerId = User.FindFirst("PartnerId")?.Value;
        if (partnerId == null)
        {
            return Unauthorized(new ApiResult()
            {
                Success = false,
                Message = "Unauthorized"
            });
        }

        var orderEntity = new Order()
        {
            PartnerId = partnerId,
            PickName = order.Order.PickName,
            PickAddress = order.Order.PickAddress,
            PickProvince = order.Order.PickProvince,
            PickDistrict = order.Order.PickDistrict,
            PickWard = order.Order.PickWard,
            PickTel = order.Order.PickTel,
            Tel = order.Order.Tel,
            Name = order.Order.Name,
            Address = order.Order.Address,
            Province = order.Order.Province,
            District = order.Order.District,
            Ward = order.Order.Ward,
            Hamlet = order.Order.Hamlet,
            IsFreeship = order.Order.IsFreeship,
            PickDate = DateTimeOffset.UtcNow,
            PickMoney = order.Order.PickMoney,
            Note = order.Order.Note,
            Value = order.Order.Value,
            Transport = order.Order.Transport,
            PickOption = order.Order.PickOption,
            DeliverOption = order.Order.DeliverOption,
            TrackingId = Guid.NewGuid().ToString(),
            
            Products = order.Products.Select(p => new Product
            {
              Name  = p.Name,
              Quantity = p.Quantity,
                Weight = p.Weight,
                ProductCode = p.ProductCode,
            }).ToList()
        };
        
        await _orderRepository.CreateOrderAsync(orderEntity);
        var response = new SubmitOrderResponse()
        {
            Success = true,
            Message = "Order submitted successfully",
            Order = new CreateOrderResponse()
            {
                PartnerId = partnerId,
                Label = "label",
                Area = 1,
                Fee = 1000,
                InsuranceFee = 0,
                TrackingId = orderEntity.TrackingId,
                EstimatedPickTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                EstimatedDeliverTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Products = order.Products.Select(p => new OrderProduct()
                {
                    Name = p.Name,
                    Weight = p.Weight,
                    Quantity = p.Quantity,
                    ProductCode = p.ProductCode
                }).ToArray(),
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
            Order = new StatusOrder()
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