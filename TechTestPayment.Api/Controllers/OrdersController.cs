using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Application.Services.Abstractions;

namespace TechTestPayment.Api.Controllers
{
    /// <summary>
    /// Default full args constructor.
    /// </summary>
    /// <param name="orderService">Order services business rules</param>
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        /// <summary>
        /// Retrieves an order
        /// </summary>
        /// <remarks>
        /// ### Requirements:
        /// - **Existing Order**: Provided id must match an existing order in database.
        /// #### Possible Status
        /// - 0: **Undefined**
        /// - 1: **WaitingPayment**
        /// - 2: **PaymentApproved**
        /// - 3: **Sent**
        /// - 4: **Delivered**
        /// - 5: **Cancelled**
        /// </remarks>
        /// <param name="id">Order unique identifier</param>
        /// <returns>Registered order</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="422">Not processable request</response>
        /// <response code="500">Unknown internal error</response>
        [HttpGet]
        public async Task<ActionResult<GetOrderResponse>> Get([FromQuery] int id)
        {
            return Ok(await orderService.Get(id));
        }

        /// <summary>
        /// Register a new order
        /// </summary>
        /// <remarks>
        /// ### Requirements:
        /// - **Existing Seller**: There must be at least one seller registered to be used on the payload.
        /// - **Existing Products**: Informed products must exist in database.
        /// - **Products in stock**: Requested products must have enough items in stock.
        /// 
        /// #### A new order is registered with the status (1) **[WaitingPayment]**.
        /// </remarks>
        /// <param name="request">Order data</param>
        /// <returns>id of registered order</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="422">Not processable request</response>
        /// <response code="500">Unknown internal error</response>
        [HttpPost]
        public async Task<ActionResult<RegisterOrderResponse>> Post(RegisterOrderRequest request)
        {
            return StatusCode((int)HttpStatusCode.Created, await orderService.Register(request));
        }

        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <remarks>
        /// ### Requirements
        /// - **Existing Order**: The order must already exist in the database.
        /// - **Valid Status Transition**: The current status of the order must allow the new requested status.
        /// 
        /// #### Supported Status Transitions
        /// - **WaitingPayment** to **[PaymentApproved]** or **[Canceled]**
        /// - **PaymentApproved** to **[Sent]** or **[Canceled]**
        /// - **Sent** to **[Delivered]**
        /// 
        /// #### Possible Status
        /// - 0: **Undefined**
        /// - 1: **WaitingPayment**
        /// - 2: **PaymentApproved**
        /// - 3: **Sent**
        /// - 4: **Delivered**
        /// - 5: **Cancelled**
        /// 
        /// These transitions ensure that the order status progresses in a logical and allowed sequence.
        /// </remarks>
        /// <param name="request">The payload containing the details for the update request.</param>
        /// <returns>The ID of the updated order.</returns>
        /// <response code="200">Success - The order status was updated successfully.</response>
        /// <response code="400">Bad Request - The request data is invalid.</response>
        /// <response code="422">Unprocessable Entity - The requested status transition is not allowed.</response>
        /// <response code="500">Internal Server Error - An unknown error occurred.</response>
        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateOrderRequest request)
        {
            await orderService.UpdateStatus(request);
            return Ok();
        }
    }
}