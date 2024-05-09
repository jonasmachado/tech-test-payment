namespace TechTestPayment.Application.Dto.Http.Response
{
    /// <summary>
    /// Represents the response of the order search.
    /// </summary>
    /// <summary>
    /// Represents the attributes of the product.
    /// </summary>
    public class GetOrderProductItem
    {
        /// <summary>
        /// Identify a product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Product amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total price
        /// </summary>
        public decimal TotalPrice => UnitPrice * (decimal)Amount;
    }
}
