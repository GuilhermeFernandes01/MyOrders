using Microsoft.AspNetCore.Mvc;
using MyOrders.Api.Responses;
using MyOrders.Api.Filters;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Inputs.Orders;
using MyOrders.Application.Outputs.Orders;
using MyOrders.Application.UseCases.Orders.Create;
using MyOrders.Application.UseCases.Orders.GetById;
using MyOrders.Application.UseCases.Orders.GetAll;
using MyOrders.Application.UseCases.Orders.UpdateById;
using System.Text.Json;

namespace MyOrders.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [OrderControllerFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync(
        [FromServices] ICreateOrderUseCase useCase,
        [FromBody] CreateOrderRequest createOrderRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("REQUEST: Create order request received {request}", JsonSerializer.Serialize(createOrderRequest));

        var orderDTO = new CreateOrderDto(createOrderRequest.ProductName, createOrderRequest.Quantity);

        _logger.LogInformation("INFO: Object mapped {object}", JsonSerializer.Serialize(orderDTO));

        var orderCreated = await useCase.Execute(orderDTO, cancellationToken).ConfigureAwait(false);

        var responseUri = new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}/Orders/{orderCreated.OrderId}/");

        _logger.LogInformation("RESPONSE: Order created successfully {response}", JsonSerializer.Serialize(responseUri));

        return Created(responseUri, default);
    }

    [HttpGet]
    [Route("{OrderId}")]
    [ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderById(
        [FromServices] IGetOrderByIdUseCase useCase,
        [FromRoute] GetOrderByIdRequest getOrderByIdRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("REQUEST: Get order by id request received {request}", JsonSerializer.Serialize(getOrderByIdRequest));

        var getOrderDTO = new GetOrderByIdDto(getOrderByIdRequest.OrderId);

        _logger.LogInformation("INFO: Object mapped {object}", JsonSerializer.Serialize(getOrderDTO));

        var response = await useCase.Execute(getOrderDTO, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("RESPONSE: Order returned successfully {response}", JsonSerializer.Serialize(response));

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllOrdersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrders(
        [FromServices] IGetOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("REQUEST: Get orders request received");

        var response = await useCase.Execute(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("RESPONSE: Orders returned successfully {response}", JsonSerializer.Serialize(response));

        return Ok(response);
    }

    [HttpPut]
    [Route("{OrderId}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderStatusAsync(
        [FromServices] IUpdateOrderByIdUseCase useCase,
        [FromRoute] UpdateOrderStatusByIdRequest updateOrderStatusByIdRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("REQUEST: Update order by id request received {request}", JsonSerializer.Serialize(updateOrderStatusByIdRequest));

        var request = new UpdateOrderStatusByIdDto(updateOrderStatusByIdRequest.OrderId);

        _logger.LogInformation("INFO: Object mapped {object}", JsonSerializer.Serialize(request));

        await useCase.Execute(request, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("RESPONSE: Message published successfully");

        return Accepted();
    }
}