using FluentValidation;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusByClientIdAndDepAddress
{
    public class GetRequestStatusByClientIdAndDepAddressQueryValidator : AbstractValidator<GetRequestStatusByClientIdAndDepAddressQuery>
    {
        public GetRequestStatusByClientIdAndDepAddressQueryValidator()
        {

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client ID cannot be empty.")
                .NotEqual(Guid.Empty).WithMessage("Client ID must be valid.");

            RuleFor(x => x.DepartmentAddress)
                .NotEmpty().WithMessage("Department address must be specified.")
                .MaximumLength(255).WithMessage("Department address must less then 255 chars");
        }
    }
}
