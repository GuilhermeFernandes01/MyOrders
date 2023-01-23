using FluentAssertions;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.UseCases.Orders.Create;
using Test.Utils.Logger;
using Test.Utils.Repositories;
using Test.Utils.DTOs.Orders;
using MyOrders.Application.UseCases.Orders.GetById;
using Test.Utils.Models;
using Moq;
using MyOrders.Domain.Models;

namespace UseCases.Test.Orders.Create;

public class CreateOrderUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request to create an order THEN the order should be created successfully")]
    public async Task ValidateSuccess()
    {
        // Arrange
        var request = CreateOrderDtoBuilder.Build();
        var cancellationToken = new CancellationToken();
        var order = OrderBuilder.Build();

        var logger = LoggerBuilder<CreateOrderUseCase>.Instance().Build();
        var repository = OrderRepositoryBuilder.Instance().AddAsync(order, cancellationToken).Build();

        var useCase = new CreateOrderUseCase(repository.Object, logger.Object);

        // Act
        var response = await useCase.Execute(request, cancellationToken);

        // Assert
        response.Should().BeOfType<CreateOrderResponse>();
        repository.Verify(x => x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        repository.VerifyNoOtherCalls();
    }
}