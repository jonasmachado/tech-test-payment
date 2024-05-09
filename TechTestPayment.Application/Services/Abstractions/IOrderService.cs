using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;

namespace TechTestPayment.Application.Services.Abstractions
{
    /// <summary>
    /// Order service application layer.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Return the sale according to the id sent.
        /// </summary>
        /// <param name="id">Identify of sale</param>
        /// <returns></returns>
        Task<GetOrderResponse> Get(int id);

        /// <summary>
        /// Register new order.
        /// </summary>
        /// <param name="request">Represents the attributes of the order registration request</param>
        /// <returns>Task completed</returns>
        Task<RegisterOrderResponse> Register(RegisterOrderRequest request);

        /// <summary>
        /// Register new order.
        /// </summary>
        /// <param name="request">Represents the attributes of the order registration request</param>
        /// <returns>Task completed</returns>
        Task UpdateStatus(UpdateOrderRequest request);
    }
}
