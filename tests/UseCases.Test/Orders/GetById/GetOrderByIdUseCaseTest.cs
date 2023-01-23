using FluentAssertions;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.UseCases.Orders.GetById;
using Test.Utils.DTOs.Orders;
using Test.Utils.Logger;
using Test.Utils.Repositories;

namespace UseCases.Test.Orders.GetById;

public class GetOrderByIdUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request THEN order should return successfully")]
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
        response.Should().BeOfType<GetOrderByIdResponse>();
    }

    private static GetOrderByIdUseCase CreateUseCase()
    {
        var logger = LoggerBuilder<GetOrderByIdUseCase>.Instance().Build();

        var repository = OrderRepositoryBuilder.Instance().Build();

        return new GetOrderByIdUseCase(repository, logger);
    }
}

