using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Cross.Extensions;

namespace TechTestPayment.Api.Controllers
{
    /// <summary>
    /// Controller used to redirect user when an exception happens, it can format the exception to a known error
    /// </summary>
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Error route, automatically redirected when an exception happens
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var path = exceptionHandlerPathFeature?.Path;
            var problemDetails = CreateProblemDetailsFromException(exceptionHandlerPathFeature?.Error, path, traceId);

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError
            };
        }

        private static ProblemDetails CreateProblemDetailsFromException(Exception? error, string? path, string? traceId)
        {
            if (error is TechTestPaymentException knownException)
            {
                return new ProblemDetails
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
                    Title = knownException.Message,
                    Detail = knownException.Details,
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Instance = path,
                    Extensions = { ["traceId"] = traceId }
                };
            }

            return new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = ErrorCodes.Generic.GetDescription(),
                Detail = error?.Message ?? "Error details unavailable.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = path,
                Extensions = { ["traceId"] = traceId }
            };
        }
    }
}
