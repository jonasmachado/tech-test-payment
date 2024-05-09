using FluentValidation;
using TechTestPayment.Application.Dto.Http.Request;

namespace TechTestPayment.Application.Validations
{
    public class UpdateOrderValidation : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderValidation()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Status).IsInEnum()
                    .WithMessage("[status] is invalid");
        }
    }
}
