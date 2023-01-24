using FluentAssertions;
using MyOrders.Domain.Models;
using MyOrders.Domain.Persistence;
using MyOrders.Infrastructure;
using MyOrders.Infrastructure.Repositories;

namespace Infrastructure.Test.Repositories;

public class OrderRepositoryTest
{
    private readonly MyOrdersDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public OrderRepositoryTest() {
        _dbContext = DependenciesInjectionMock.Build();
        _unitOfWork = DependenciesInjectionMock.Build(_dbContext);
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN AddAsync is called THEN it should create a new order")]
    public async Task ValidateAddAsyncSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var repository = new OrderRepository(_dbContext, _unitOfWork);
        var order = new Order
        {
            ProductName = "newProduct",
            Quantity = 2
        };

        // Act
        await repository.AddAsync(order, cancellationToken);

        // Assert
        order.OrderId.Should().Be(4);
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN GetOrdersByIdAsync is called with an existent orderId THEN it should return the order")]
    public async Task ValidateGetOrderByIdSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.GetOrderByIdAsync(1, cancellationToken);

        // Assert
        response.Should().BeOfType<Order>();
        response.OrderId.Should().Be(1);
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN GetOrdersAsync is called THEN it should return orders")]
    public async Task ValidateGetOrdersSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.GetOrdersAsync(cancellationToken);

        // Assert
        response.Should().NotBeNullOrEmpty();
        response.Count().Should().Be(3);
        response.Select(r => r.Should().BeOfType<Order>());
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN CheckOrderIdExists is called and order exists THEN it should return true")]
    public async Task ValidateCheckOrderIdExistsSuccess()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.CheckOrderIdExists(1, cancellationToken);

        // Assert
        response.Should().BeTrue();
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN MarkOrderAsPaidAsync is called and order exists THEN it should update Paid field to true")]
    public async Task ValidateMarkOrderAsPaidAsyncSuccess()
    {
        // Arrange
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.MarkOrderAsPaidAsync(1);

        // Assert
        response.Should().BeOfType<Order>();
        response.Paid.Should().BeTrue();
        response.Shipped.Should().BeFalse();
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN MarkOrderAsPaidAsync is called and order does not exist THEN it should return null")]
    public async Task ValidateMarkOrderAsPaidAsyncFailureOrderDoesNotExist()
    {
        // Arrange
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.MarkOrderAsPaidAsync(9999);

        // Assert
        response.Should().BeNull();
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN MarkOrderAsPaidAsync is called and order exists THEN it should update Paid field to true")]
    public async Task ValidateMarkOrderAsShippedAsyncSuccess()
    {
        // Arrange
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.MarkOrderAsShippedAsync(1);

        // Assert
        response.Should().BeOfType<Order>();
        response.Paid.Should().BeFalse();
        response.Shipped.Should().BeTrue();
    }

    [Fact(DisplayName = "GIVEN a valid repository WHEN MarkOrderAsShippedAsync is called and order does not exist THEN it should return null")]
    public async Task ValidateMarkOrderAsShippedAsyncFailureOrderDoesNotExist()
    {
        // Arrange
        var repository = new OrderRepository(_dbContext, _unitOfWork);

        // Act
        var response = await repository.MarkOrderAsShippedAsync(9999);

        // Assert
        response.Should().BeNull();
    }
}

