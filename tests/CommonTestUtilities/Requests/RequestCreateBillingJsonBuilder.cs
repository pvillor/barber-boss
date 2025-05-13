using BarberBoss.Communication.Requests;
using Bogus;
using CashFlow.Communication.Enums;

namespace CommonTestUtilities.Requests;

public class RequestCreateBillingJsonBuilder
{
    public static RequestBillingJson Build()
    {
        return new Faker<RequestBillingJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}
