namespace TechTestPayment.Domain.Abstractions.Services
{
    /// <summary>
    /// Abstraction for product service.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Performs the product registration.
        /// </summary>
        /// <param name="productIds">Ids</param>
        /// <param name="quantities">Amount</param>
        /// <returns></returns>
        Task PerformStockReduction(List<int> productIds, Dictionary<int, double> quantities);
    }
}