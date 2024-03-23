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
                .NotEmpty().WithMessage("Department address must be specified.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount must be specified.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency must be specified.");

        }
    }
}
