using BarberBoss.Application.UseCases.Billings;
using BarberBoss.Exception;
using CashFlow.Communication.Enums;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Billings.Create;

public class CreateBillingValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new BillingValidator();
        var request = RequestCreateBillingJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new BillingValidator();
        var request = RequestCreateBillingJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Date_In_Future()
    {
        var validator = new BillingValidator();
        var request = RequestCreateBillingJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage.Equals(ResourceErrorMessages.BILLINGS_CANNOT_FOR_FUTURE));
    }

    [Fact]
    public void Error_Invalid_Payment_Type()
    {
        var validator = new BillingValidator();
        var request = RequestCreateBillingJsonBuilder.Build();
        request.PaymentType = (PaymentType) 300;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PAYMENT_TYPE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Amount_Not_Greater_Than_Zero(decimal amount)
    {
        var validator = new BillingValidator();
        var request = RequestCreateBillingJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage.Equals(ResourceErrorMessages.BILLINGS_GREATER_THAN_ZERO));
    }
}