using FluentValidation;

namespace CashRequestApi.Core.Requests.Queries.GetRequestStatusById
{
    public class GetRequestStatusByIdQueryValidator : AbstractValidator<GetRequestStatusByIdQuery>
    {
        public GetRequestStatusByIdQueryValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty().WithMessage("Request ID cannot be empty.")
                .NotEqual(Guid.Empty).WithMessage("Request ID must be valid.");
        }
    }
}
