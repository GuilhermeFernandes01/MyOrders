using FluentAssertions;
using Moq;
using MyOrders.Application.Outputs.Orders;
using MyOrders.Application.UseCases.Orders.GetById;
using MyOrders.Domain.Exceptions;
using Test.Utils.DTOs.Orders;
using Test.Utils.Logger;
using Test.Utils.Repositories;

namespace UseCases.Test.Orders.GetById;

public class GetOrderByIdUseCaseTest
{
    [Fact(DisplayName = "GIVEN a valid use case WHEN there is a valid request to get an order by id THEN the order should be returned successfully")]
    public async Task ValidateSuccess()
    {
        // Arrange
        var request = GetOrderByIdDtoBuilder.Build();
        var cancellationToken = new CancellationToken();

        var logger = LoggerBuilder<GetOrderByIdUseCase>.Instance().Build();
        var repository = OrderRepositoryBuilder.Instance().GetOrderByIdAsync(request.OrderId, cancellationToken).Build();

        var useCase = new GetOrderByIdUseCase(repository.Object, logger.Object);

        // Act
        var response = await useCase.Execute(request, cancellationToken);

        // Assert
        response.Should().BeOfType<GetOrderByIdResponse>();
        repository.Verify(x => x.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        repository.VerifyNoOtherCalls();
    }
}

