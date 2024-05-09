using FluentValidation;
using TechTestPayment.Application.Dto.Http.Request;

namespace TechTestPayment.Application.Validations
{
    public class RegisterProductValidation : AbstractValidator<RegisterProductRequest>
    {
        public RegisterProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("[Name] Cannot be null.")
                .MaximumLength(100).WithMessage("[Name] Max length is 100.");
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.ItemsRemaining).GreaterThanOrEqualTo(0);
        }
    }
}
