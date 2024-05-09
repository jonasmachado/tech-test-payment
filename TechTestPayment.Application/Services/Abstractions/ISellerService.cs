using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;

namespace TechTestPayment.Application.Services.Abstractions
{
    /// <summary>
    /// Application service for seller.
    /// </summary>
    public interface ISellerService
    {
        /// <summary>
        /// Register a new seller.
        /// </summary>
        /// <param name="request">Seller creation data</param>
        /// <returns>Registered Seller Response containing the created id.</returns>
        Task<RegisterSellerResponse> Register(RegisterSellerRequest request);
    }
}