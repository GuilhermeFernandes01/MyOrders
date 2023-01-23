using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MyOrders.Application.Outputs.Orders;
using MyOrders.Application.UseCases.Orders.UpdateById;
using MyOrders.Domain.Contracts;
using Test.Utils.Bus;
using Test.Utils.DTOs.Orders;
using Test.Utils.Logger;
using Test.Utils.Repositories;

namespace UseCases.Test.Orders.UpdateById;

public class UpdateOrderByIdUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request to update order status by id THEN order should be sent to RabbitMQ successfully")]
    public async Task ValidateSuccess()
    {
        // Arrange
        var request = UpdateOrderStatusByIdDtoBuilder.Build();
        var cancellationToken = new CancellationToken();

        var repository = OrderRepositoryBuilder
            .Instance()
            .CheckOrderIdExists(request.OrderId, cancellationToken)
            .Build();
        var logger = LoggerBuilder<UpdateOrderByIdUseCase>.Instance().Build();
        var bus = BusBuilder.Instance().Build();
        
        var useCase = new UpdateOrderByIdUseCase(repository.Object, logger.Object, bus.Object);

        // Act
        var response = await useCase.Execute(request, cancellationToken);

        // Assert
        response.Should().Be(null);
        repository.Verify(x => x.CheckOrderIdExists(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        logger.VerifyLog(LogLevel.Information, Times.Exactly(1), "DB_RESPONSE");
        logger.VerifyLog(LogLevel.Information, Times.Exactly(1), "INFO: Message published");
        bus.Verify(x => x.Publish(It.IsAny<OrderPaymentConfirmedMessage>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}