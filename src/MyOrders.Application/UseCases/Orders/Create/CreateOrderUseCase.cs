﻿using Microsoft.Extensions.Logging;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;
using MyOrders.Domain.Models;

namespace MyOrders.Application.UseCases.Orders.Create
{
	public class CreateOrderUseCase : ICreateOrderUseCase
    {
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<CreateOrderUseCase> _logger;

        public CreateOrderUseCase(
			IOrderRepository orderRepository,
			ILogger<CreateOrderUseCase> logger)
		{
			_orderRepository = orderRepository;
			_logger = logger;
        }

		public async Task<CreateOrderResponse> Execute(CreateOrderDto CreateOrderDto, CancellationToken cancellationToken)
		{
			var order = new Order
			{
				ProductName = CreateOrderDto.ProductName,
				Quantity = CreateOrderDto.Quantity,
				Paid = false,
				Shipped = false
			};

			await _orderRepository.AddAsync(order, cancellationToken).ConfigureAwait(false);

			_logger.LogInformation("INFO: Order created sucessfully {order}", order);

			var response = new CreateOrderResponse(order.OrderId);

			return response;
        }
    }
}

