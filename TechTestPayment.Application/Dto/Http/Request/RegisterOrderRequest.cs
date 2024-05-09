using System.Text.Json.Serialization;

namespace TechTestPayment.Application.Dto.Http.Request
{
    /// <summary>
    /// Represents the attributes of the order registration request.
    /// </summary>
    public record RegisterOrderRequest
    {
        /// <summary>
        /// Identifies the seller/salesperson.
        /// </summary>
        [JsonPropertyName("sellerId")]
        public int SellerId { get; set; }

        /// <summary>
        /// Attributes of the products.
        /// </summary>
        [JsonPropertyName("items")]
        public List<ProductItem>? ProductsItems { get; set; }


        /// <summary>
        /// Attributes of the product item.
        /// </summary>
        public class ProductItem
        {
            /// <summary>
            /// Identifies product.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Amount product.
            /// </summary>
            public double Amount { get; set; }
        }
    }
}
