using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;

namespace TechTestPayment.Application.Services.Abstractions
{
    /// <summary>
    /// Application product service abstraction.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Performs the product registration.
        /// </summary>
        /// <param name="request">Product information</param>
        /// <returns>Task completion with response containing the created id</returns>
        Task<RegisterProductResponse> Register(RegisterProductRequest request);
    }
}