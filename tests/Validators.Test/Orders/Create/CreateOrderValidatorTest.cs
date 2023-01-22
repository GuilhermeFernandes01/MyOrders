using FluentAssertions;
using MyOrders.Application.Validators.Orders;
using Test.Utils.Requests;

namespace Validators.Test.Orders.Create;

public class CreateOrderValidatorTest
{
    [Fact(DisplayName = "GIVEN a valid validator WHEN there is a valid product THEN it should return success")]
    public void ValidateSuccess()
    {
        // Arrange
        var validator = new CreateOrderValidator();

        var request = CreateOrderRequestBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact(DisplayName = "GIVEN a valid validator WHEN product name is empty THEN it should return an error")]
    public void ValidateEmptyProductName()
    {
        // Arrange
        var validator = new CreateOrderValidator();

        var request = CreateOrderRequestBuilder.Build();
        request.ProductName = string.Empty;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals("'Product Name' must not be empty."));
    }

    [Fact(DisplayName = "GIVEN a valid validator WHEN quantity is less than one THEN it should return an error")]
    public void ValidateQuantityLessThanOne()
    {
        // Arrange
        var validator = new CreateOrderValidator();

        var request = CreateOrderRequestBuilder.Build();
        request.Quantity = 0;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals("'Quantity' must be greater than '0'."));
    }
}
