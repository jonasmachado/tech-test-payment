using System.Text.Json.Serialization;

namespace TechTestPayment.Application.Dto.Http.Request
{
    /// <summary>
    /// Represents the attributes of the product registration request.
    /// </summary>
    public record RegisterProductRequest
    {
        /// <summary>
        /// Product name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Items remaining in stock.
        /// </summary>
        [JsonPropertyName("remaining")]
        public double ItemsRemaining { get; set; }

        /// <summary>
        /// Total price of the product.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
