using FluentAssertions;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.UseCases.Orders.GetAll;
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

        var useCase = CreateUseCase();

        // Act
        var response = await useCase.Execute(cancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<GetAllOrdersResponse>();
    }

    private static GetOrdersUseCase CreateUseCase()
    {
        var logger = LoggerBuilder<GetOrdersUseCase>.Instance().Build();

        var repository = OrderRepositoryBuilder.Instance().Build();

        return new GetOrdersUseCase(repository, logger);
    }
}

