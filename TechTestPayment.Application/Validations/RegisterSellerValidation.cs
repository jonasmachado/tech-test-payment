using FluentValidation;
using TechTestPayment.Application.Dto.Http.Request;

namespace TechTestPayment.Application.Validations
{
    public class RegisterSellerValidation : AbstractValidator<RegisterSellerRequest>
    {
        public RegisterSellerValidation()
        {
            RuleFor(x => x.Cpf)
                .NotNull().WithMessage("[CPF] Must be informed.")
                .Length(11).WithMessage("[CPF] The field must be 11 characters long.")
                .Matches("^[0-9]*$").WithMessage("[CPF] Must contain only numbers.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("[Name] Must be informed.")
                .Length(3, 100).WithMessage("[Name] Must contain between 3 and 100 characters.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("[Email] Must be informed.")
                .EmailAddress().WithMessage("[Email] Must be a valid email address.")
                .MaximumLength(100).WithMessage("[Email] Must contain no more than 100 characters.");

            RuleFor(x => x.Phone)
                .NotNull().WithMessage("[Phone] Must be informed.")
                .Length(6, 20).WithMessage("[Phone] Must contain between 6 to 20 characters")
                .Matches("^[0-9]*$").WithMessage("[Phone] Must contain only numbers.");
        }
    }
}
