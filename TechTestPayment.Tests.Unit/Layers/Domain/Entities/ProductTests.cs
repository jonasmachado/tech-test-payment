using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Tests.Unit.Layers.Domain.Entities
{
    public class ProductTests
    {
        private readonly Product _product = new();

        [Fact]
        public void Product_WithdrawStock_ThrowsWhenInsufficientAmount()
        {
            _product.ItemsRemaining = 10;

            var exception = Assert.Throws<BusinessException>(() => _product.WithdrawStock(15));

            Assert.Equal("[POT-008] The stock is insufficient for the order.", exception.Message);
        }
    }
}
