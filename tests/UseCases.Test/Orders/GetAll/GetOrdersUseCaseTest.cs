using FluentAssertions;
using Moq;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.UseCases.Orders.GetAll;
using MyOrders.Domain.Models;
using Test.Utils.Logger;
using Test.Utils.Repositories;

namespace UseCases.Test.Orders.GetAll;

public class GetOrdersUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request THEN orders should return successfully")]
    public async Task ValidateSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();

        var logger = LoggerBuilder<GetOrdersUseCase>.Instance().Build();
        var repository = OrderRepositoryBuilder.Instance().GetOrdersAsync(cancellationToken).Build();

        // Act
        var useCase = new GetOrdersUseCase(repository.Object, logger.Object);

        var response = await useCase.Execute(cancellationToken);

        // Assert
        response.Should().BeOfType<GetAllOrdersResponse>();
        response.Total.Should().Be(2);
        response.Orders.Select(x => x.Should().BeOfType<Order>());
        repository.Verify(x => x.GetOrdersAsync(It.IsAny<CancellationToken>()), Times.Once);
        repository.VerifyNoOtherCalls();
    }
}

