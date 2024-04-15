using FluentValidation;

namespace Ordering.Application.Features.V1.Order.Commands.CreateCommand
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.UserName).NotEmpty().WithMessage("{UserName} is required.").NotNull().MaximumLength(50).WithMessage("{UserName} must not exceed 50 character.");
            RuleFor(o => o.EmailAddress).NotEmpty().WithMessage("{EmailAddress} is required.").NotNull().EmailAddress().WithMessage("{EmailAddress} is not valid.");
            RuleFor(o => o.ToatalPrice).NotEmpty().WithMessage("{ToatalPrice} is required.").GreaterThan(0).WithMessage("{ToatalPrice} must be greater than 0.");
        }
    }
}
