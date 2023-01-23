﻿using FluentAssertions;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.UseCases.Orders.Create;
using Test.Utils.Logger;
using Test.Utils.Repositories;
using Test.Utils.DTOs.Orders;
using MyOrders.Application.UseCases.Orders.GetById;

namespace UseCases.Test.Orders.Create;

public class CreateOrderUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request THEN an order should be created successfully")]
    public async Task ValidateSuccess()
    {
        // Arrange
        var request = GetOrderByIdDtoBuilder.Build();

        var cancellationToken = new CancellationToken();

        var useCase = CreateUseCase();

        // Act
        var response = await useCase.Execute(request, cancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<CreateOrderResponse>();
    }

    private static GetOrderByIdUseCase CreateUseCase()
	{
		var logger = LoggerBuilder<GetOrderByIdUseCase>.Instance().Build();

        var repository = OrderRepositoryBuilder.Instance().Build();

		return new GetOrderByIdUseCase(repository, logger);
    }
}