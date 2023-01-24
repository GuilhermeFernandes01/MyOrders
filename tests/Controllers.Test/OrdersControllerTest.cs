using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrders.Api.Controllers;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Inputs.Orders;
using MyOrders.Application.Outputs.Orders;
using MyOrders.Application.UseCases.Orders.Create;
using MyOrders.Application.UseCases.Orders.GetById;
using MyOrders.Application.UseCases.Orders.GetAll;
using MyOrders.Application.UseCases.Orders.UpdateById;
using MyOrders.Domain.Models;
using Test.Utils.Models;

namespace Controllers.Test;

public class OrdersControllerTest
{
    [Fact(DisplayName = "GIVEN a request to [POST] order WHEN request is valid THEN it should create the order")]
    public async Task ValidatePostAsyncSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var useCase = new Mock<ICreateOrderUseCase>();
        var logger = new Mock<ILogger<OrdersController>>();

        var request = new CreateOrderRequest{
            ProductName = "newProduct",
            Quantity = 1
        };

        var createOrderDto = new CreateOrderDto(request.ProductName, request.Quantity);

        useCase.Setup(x => x.Execute(It.IsAny<CreateOrderDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateOrderResponse(1))
            .Verifiable();

        var controller = new OrdersController(logger.Object)
        {
            ControllerContext =
            {
                HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Scheme = "https",
                        Host = new HostString("localhost"),
                        PathBase = new PathString("/v1/api")
                    }
                }
            }
        };

        // Act
        var response = await controller.PostAsync(useCase.Object, request, cancellationToken);

        // Assert
        response.Should().BeOfType<CreatedResult>();
        useCase.Verify();
        useCase.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "GIVEN a request to [GET] an order by id WHEN request is valid THEN it should return the order")]
    public async Task ValidateGetOrderByIdSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var useCase = new Mock<IGetOrderByIdUseCase>();
        var logger = new Mock<ILogger<OrdersController>>();

        var request = new GetOrderByIdRequest
        {
            OrderId = 1
        };

        var getOrderByIdDto = new GetOrderByIdDto(request.OrderId);

        useCase.Setup(x => x.Execute(It.IsAny<GetOrderByIdDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetOrderByIdResponse(new Order
            {
                OrderId = request.OrderId,
            }))
            .Verifiable();

        var controller = new OrdersController(logger.Object);

        // Act
        var response = await controller.GetOrderById(useCase.Object, request, cancellationToken);

        // Assert
        response.Should().BeOfType<OkObjectResult>();
        useCase.Verify();
        useCase.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "GIVEN a request to [GET] orders WHEN request is valid THEN it should return the orders")]
    public async Task ValidateGetOrdersSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var useCase = new Mock<IGetOrdersUseCase>();
        var logger = new Mock<ILogger<OrdersController>>();

        IEnumerable<Order> orders = new List<Order> { OrderBuilder.Build(), OrderBuilder.Build() };

        useCase.Setup(x => x.Execute(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAllOrdersResponse(orders.Count(), orders))
            .Verifiable();

        var controller = new OrdersController(logger.Object);

        // Act
        var response = await controller.GetOrders(useCase.Object, cancellationToken);

        // Assert
        response.Should().BeOfType<OkObjectResult>();
        useCase.Verify();
        useCase.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "GIVEN a request to [PUT] an order by id WHEN request is valid THEN it should send the order to RabbitMQ")]
    public async Task ValidateUpdateOrderStatusAsyncSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var useCase = new Mock<IUpdateOrderByIdUseCase>();
        var logger = new Mock<ILogger<OrdersController>>();

        var request = new UpdateOrderStatusByIdRequest
        {
            OrderId = 1
        };

        var updateOrderStatusByIdDto = new UpdateOrderStatusByIdDto(request.OrderId);

        useCase.Setup(x => x.Execute(It.IsAny<UpdateOrderStatusByIdDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UpdateOrderByIdResponse())
            .Verifiable();

        var controller = new OrdersController(logger.Object);

        // Act
        var response = await controller.UpdateOrderStatusAsync(useCase.Object, request, cancellationToken);

        // Assert
        response.Should().BeOfType<AcceptedResult>();
        useCase.Verify();
        useCase.VerifyNoOtherCalls();
    }
}
