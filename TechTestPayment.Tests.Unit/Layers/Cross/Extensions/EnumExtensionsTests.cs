using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Extensions;

namespace TechTestPayment.Tests.Unit.Layers.Cross.Extensions
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void GetDescription_WhenCalled_ReturnsDescription()
        {
            var enumValue = ErrorCodes.InsufficientStock;

            var description = enumValue.GetDescription();
            Assert.Equal("[POT-008] The stock is insufficient for the order.", description);
        }
    }
}
