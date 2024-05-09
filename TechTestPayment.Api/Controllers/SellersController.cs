using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;

namespace TechTestPayment.Api.Controllers
{
    /// <summary>
    /// Default full args constructor.
    /// </summary>
    /// <param name="sellerService">Seller services business rules</param>
    [ApiController]
    [Route("[controller]")]
    public class SellersController(ISellerService sellerService) : ControllerBase
    {
        /// <summary>
        /// Register a new seller.
        /// </summary>
        /// <remarks>
        /// ### Requirements:
        /// - **Document be unique**: Provided document number must not be registered yet.
        /// </remarks>
        /// <param name="request">Request payload</param>
        /// <returns>Registered seller id</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="422">Not processable request</response>
        /// <response code="500">Unknown internal error</response>
        [HttpPost]
        public async Task<IActionResult> Post(RegisterSellerRequest request)
        {
            return StatusCode((int)HttpStatusCode.Created, await sellerService.Register(request));
        }
    }
}