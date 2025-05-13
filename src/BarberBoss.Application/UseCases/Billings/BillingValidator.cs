using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Billings;

public class BillingValidator : AbstractValidator<RequestBillingJson>
{
    public BillingValidator()
    {
        RuleFor(billing => billing.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(billing => billing.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.BILLINGS_GREATER_THAN_ZERO);
        RuleFor(billing => billing.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.BILLINGS_CANNOT_FOR_FUTURE);
        RuleFor(billing => billing.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_PAYMENT_TYPE);
    }
}
