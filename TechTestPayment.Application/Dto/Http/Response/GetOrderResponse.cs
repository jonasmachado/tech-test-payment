namespace TechTestPayment.Application.Dto.Http.Response
{
    /// <summary>
    /// Represents the response of the sale search.
    /// </summary>
    public class GetOrderResponse
    {
        /// <summary>
        /// Identify a sale.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identify a seller.
        /// </summary>
        public int SellerId { get; set; }

        /// <summary>
        /// Date of the sale.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Status of the sale.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Total price
        /// </summary>
        public decimal TotalPrice => ProductItems?.Sum(x => x.TotalPrice) ?? 0;

        /// <summary>
        /// List of products.
        /// </summary>
        public List<GetOrderProductItem>? ProductItems { get; set; }
    }
}
