using Microsoft.Extensions.Logging;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.Services;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Domain.Services
{
    public class ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger) : IProductService
    {
        public async Task PerformStockReduction(List<int> productIds, Dictionary<int, double> quantities)
        {
            logger.LogInformation("Performing stock change");

            var products = await GetProductsByIds(productIds);
            ValidateProductsExist(products, productIds);

            foreach (var product in products)
            {
                UpdateProductStock(product, quantities);
            }

            await unitOfWork.SaveChangesAsync();
        }

        private async Task<List<Product>> GetProductsByIds(List<int> productIds)
        {
            return await unitOfWork.ProductRepository.FindAsync(x => productIds.Contains(x.Id));
        }

        private void ValidateProductsExist(List<Product> products, List<int> productIds)
        {
            var foundIds = products.Select(p => p.Id).ToList();
            var missingIds = productIds.Except(foundIds).ToList();

            if (missingIds.Count != 0)
            {
                logger.LogError("Products with IDs {MissingIds} were not found in the database", missingIds);
                throw new DatabaseException(ErrorCodes.ProductDoesNotExist);
            }
        }

        private void UpdateProductStock(Product product, Dictionary<int, double> quantities)
        {
            var orderAmount = quantities[product.Id];
            product.WithdrawStock(orderAmount);
            unitOfWork.SetModified(product);
        }
    }
}
