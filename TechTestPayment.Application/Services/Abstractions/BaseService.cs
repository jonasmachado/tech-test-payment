using FluentValidation;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;

namespace TechTestPayment.Application.Services.Abstractions
{
    /// <summary>
    /// Base service class.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Validates incoming request object information.
        /// </summary>
        /// <typeparam name="T">Request type</typeparam>
        /// <param name="validator">Fluent validation class</param>
        /// <param name="toValidate">Request to be validated</param>
        /// <exception cref="RequestValidationException">Possible invalid request data</exception>
        protected static void ValidateRequest<T>(IValidator<T> validator, T toValidate)
        {
            var result = validator.Validate(toValidate);

            if (result.IsValid)
                return;

            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();

            throw new RequestValidationException(ErrorCodes.InvalidRequestData, string.Join(Environment.NewLine, errors));
        }
    }

}
