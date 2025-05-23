using System.Text.Json.Serialization;

namespace Ghtk.Api.Models;

public partial class SubmitOrderResponse : ApiResult
{
    [JsonPropertyName("order")]
    public CreateOrderResponse Order { get; set; }
}

public partial class CreateOrderResponse
{
    [JsonPropertyName("partner_id")]
    public string PartnerId { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonPropertyName("area")]
    public long Area { get; set; }

    [JsonPropertyName("fee")]
    public long Fee { get; set; }

    [JsonPropertyName("insurance_fee")]
    public double InsuranceFee { get; set; }

    [JsonPropertyName("tracking_id")]
    public long TrackingId { get; set; }

    [JsonPropertyName("estimated_pick_time")]
    public string EstimatedPickTime { get; set; }

    [JsonPropertyName("estimated_deliver_time")]
    public string EstimatedDeliverTime { get; set; }

    [JsonPropertyName("products")]
    public Product[] Products { get; set; }

    [JsonPropertyName("status_id")]
    public long StatusId { get; set; }
}