using FluentAssertions;
using MyOrders.Application.UseCases.Order.Create;
using Test.Utils.Requests;

namespace Validators.Test.Orders.Create
{
    public class CreateOrderValidatorTest
    {
        [Fact]
        public void ValidateSuccess()
        {
            var validator = new CreateOrderValidator();

            var request = CreateOrderRequestBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ValidateEmptyProductName()
        {
            var validator = new CreateOrderValidator();

            var request = CreateOrderRequestBuilder.Build();
            request.ProductName = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals("'Product Name' must not be empty."));
        }

        [Fact]
        public void ValidateQuantityLessThanOne()
        {
            var validator = new CreateOrderValidator();

            var request = CreateOrderRequestBuilder.Build();
            request.Quantity = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals("'Quantity' must be greater than '0'."));
        }
    }
}
