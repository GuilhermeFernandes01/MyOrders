using FluentAssertions;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.Outputs.Orders;
using MyOrders.Application.UseCases.Orders.GetById;
using MyOrders.Application.UseCases.Orders.UpdateById;
using Test.Utils.Bus;
using Test.Utils.DTOs.Orders;
using Test.Utils.Logger;
using Test.Utils.Repositories;

namespace UseCases.Test.Orders.UpdateById;

public class UpdateOrderByIdUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request THEN order should be sent and returned successfully")]
    public async Task ValidateSuccess()
    {
        // Arrange
        var request = UpdateOrderStatusByIdDtoBuilder.Build();

        var cancellationToken = new CancellationToken();

        var useCase = CreateUseCase();

        // Act
        var response = await useCase.Execute(request, cancellationToken);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<UpdateOrderByIdResponse>();
    }

    private static UpdateOrderByIdUseCase CreateUseCase()
    {
        var logger = LoggerBuilder<UpdateOrderByIdUseCase>.Instance().Build();

        var bus = BusBuilder.Instance().Build();

        var repository = OrderRepositoryBuilder.Instance().Build();

        return new UpdateOrderByIdUseCase(repository, logger, bus);
    }
}