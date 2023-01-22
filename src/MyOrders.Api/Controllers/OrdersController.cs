﻿using Microsoft.AspNetCore.Mvc;
using MyOrders.Api.Responses;
using MyOrders.Application.Orders.Inputs;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Application.UseCases.Order.Create;
using MyOrders.Application.UseCases.Order.GetById;
using MyOrders.Application.UseCases.Order.GetAll;
using MyOrders.Application.UseCases.Order.UpdateById;

namespace MyOrders.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync(
        [FromServices] ICreateOrderUseCase useCase,
        [FromBody] CreateOrderRequest createOrderRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("REQUEST: Create order request received {request}", createOrderRequest);

        var orderId = await useCase.Execute(createOrderRequest, cancellationToken);

        var responseUri = new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}/Orders/{orderId}/");

        _logger.LogInformation("RESPONSE: Order created successfully {response}", responseUri);

        return Created(responseUri, default);
    }

    [HttpGet]
    /*
     * Eu não utilizaria "orderId", pois já estamos no contexto de order e /orders/{id}
     * já seria suficiente para entender que estamos buscando uma order por seu id.
     * No banco de dados também modificaria de "orderId" para "id".
     * Usar "orderId" neste caso é redundante, tornando-se questionável se a rota segue
     * o padrão REST por não aproveitar de sua estrutura hierárquica.
     */
    [Route("{OrderId}")]
    [ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderById(
        [FromServices] IGetOrderByIdUseCase useCase,
        [FromRoute] GetOrderByIdRequest getOrderByIdRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("REQUEST: Get order by id request received {request}", getOrderByIdRequest);

        var response = await useCase.Execute(getOrderByIdRequest, cancellationToken);

        _logger.LogInformation("RESPONSE: Order returned successfully {response}", response);

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

        var response = await useCase.Execute(cancellationToken);

        _logger.LogInformation("RESPONSE: Orders returned successfully {response}", response);

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
        _logger.LogInformation("REQUEST: Update order by id request received {request}", updateOrderStatusByIdRequest);

        await useCase.Execute(updateOrderStatusByIdRequest, cancellationToken);

        _logger.LogInformation("RESPONSE: Message published successfully");

        return Accepted();
    }
}