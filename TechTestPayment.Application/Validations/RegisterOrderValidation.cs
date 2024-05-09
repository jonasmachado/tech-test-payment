using FluentValidation;
using TechTestPayment.Application.Dto.Http.Request;

namespace TechTestPayment.Application.Validations
{
    public class RegisterOrderValidation : AbstractValidator<RegisterOrderRequest>
    {
        public RegisterOrderValidation()
        {
            RuleFor(x => x.ProductsItems).NotEmpty().WithMessage("[items] Cannot be null or empty.");
            RuleFor(x => x.SellerId).GreaterThanOrEqualTo(0);
        }
    }
}
