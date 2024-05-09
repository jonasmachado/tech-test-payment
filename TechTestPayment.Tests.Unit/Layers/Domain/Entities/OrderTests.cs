using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Entities;
using TechTestPayment.Domain.Enums;

namespace TechTestPayment.Tests.Unit.Layers.Domain.Entities
{
    public class OrderTests
    {
        private readonly Order _order = new();

        [Fact]
        public void Order_DefaultCtor_SetsDateToNow()
        {
            var expected = DateTime.Now;
            var actual = _order.Date;

            Assert.Equal(expected.Date, actual.Date);
        }

        [Fact]
        public void Order_DefaultCtor_SetsStatusToWaitingPayment()
        {
            var expected = OrderStatus.WaitingPayment;
            var actual = _order.Status;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Order_DefaultCtor_InitializesOrderProductsCollection()
        {
            var expected = 0;
            var actual = _order.OrderProducts.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Order_UpdateStatus_ThrowsWhenInvalidTransition()
        {
            _order.Status = OrderStatus.WaitingPayment;

            var exception = Assert.Throws<BusinessException>(() => _order.UpdateStatus(OrderStatus.Sent));

            Assert.Equal("[POT-004] Not allowed to change from status [Waiting for Payment] to requested status [Sent].", exception.Message);
        }
    }
}
