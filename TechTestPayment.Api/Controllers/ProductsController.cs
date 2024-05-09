using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;

namespace TechTestPayment.Api.Controllers
{
    /// <summary>
    /// Default full args constructor.
    /// </summary>
    /// <param name="productService">products business rules</param>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        /// <summary>
        /// Register new product.
        /// </summary>
        /// <remarks>
        /// ### Requirements:
        /// - **Product name be unique**: Provided product name must not be registered yet.
        /// </remarks>
        /// <param name="request">Request payload</param>
        /// <returns>Registered order</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="422">Not processable request</response>
        /// <response code="500">Unknown internal error</response>
        [HttpPost]
        public async Task<IActionResult> Post(RegisterProductRequest request)
        {
            return StatusCode((int)HttpStatusCode.Created, await productService.Register(request));
        }
    }
}