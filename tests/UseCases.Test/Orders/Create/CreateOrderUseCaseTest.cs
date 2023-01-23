using FluentAssertions;
using MyOrders.Application.UseCases.Orders.Create;
using Test.Utils.Logger;
using Test.Utils.Repositories;
using Test.Utils.Requests;
using Test.Utils.DTOs;
using Test.Utils.DTOs.Orders;
using MyOrders.Application.Orders.Outputs;

namespace UseCases.Test.Orders.Create;

public class CreateOrderUseCaseTest
{
	[Fact]
	public async Task ValidateSuccess()
    {
        // Arrange
        var request = CreateOrderDtoBuilder.Build();

        var cancellationToken = new CancellationToken();

        var useCase = CreateUseCase();

        // Act
        var response = await useCase.Execute(request, cancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<CreateOrderResponse>();
    }

    private static CreateOrderUseCase CreateUseCase()
	{
		var logger = LoggerBuilder<CreateOrderUseCase>.Instance().Build();

        var repository = OrderRepositoryBuilder.Instance().Build();

		return new CreateOrderUseCase(repository, logger);
    }
}

