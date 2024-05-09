using System.Text.Json.Serialization;
using TechTestPayment.Domain.Enums;

namespace TechTestPayment.Application.Dto.Http.Request
{
    /// <summary>
    /// Represents the request for updating an order.
    /// </summary>
    public record UpdateOrderRequest
    {
        /// <summary>
        /// Identifies the order.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// New order status.
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
    }
}
