using System.Text.Json.Serialization;

namespace TechTestPayment.Application.Dto.Http.Request
{
    /// <summary>
    /// Represents the attributes of the seller registration request.
    /// </summary>
    public record RegisterSellerRequest
    {
        /// <summary>
        /// Seller document number.
        /// </summary>
        [JsonPropertyName("cpf")]
        public string? Cpf { get; set; }

        /// <summary>
        /// Seller name.
        /// </summary>
        [JsonPropertyName("nome")]
        public string? Name { get; set; }

        /// <summary>
        /// Seller e-mail.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Seller phone number.
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}
