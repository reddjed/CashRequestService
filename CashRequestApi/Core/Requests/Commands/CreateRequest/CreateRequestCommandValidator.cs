using FluentValidation;

namespace CashRequestApi.Core.Requests.Commands.CreateRequest
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client ID cannot be empty.")
                .NotEqual(Guid.Empty).WithMessage("Client ID must be valid.");

            RuleFor(x => x.DepartmentAddress)
                .NotEmpty().WithMessage("Department address must be specified.")
                .MaximumLength(255).WithMessage("Department address must less then 255 chars");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount must be specified.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .PrecisionScale(18, 2, false).WithMessage("Amount should have no more than two decimal places and not exceed a total of 18 digits."); ;

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency must be specified.")
                .Must(currency => currency == currency.ToUpper()).WithMessage("Currency must be in uppercase.")
                .MaximumLength(3).WithMessage("Max currency length is 3 chars"); ;

        }
    }
}
