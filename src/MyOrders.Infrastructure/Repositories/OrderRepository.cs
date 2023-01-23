using MyOrders.Domain.Models;
using MyOrders.Domain.Persistence;

namespace MyOrders.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MyOrdersDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public OrderRepository(MyOrdersDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> AddOrUpdateAsync(Order order, CancellationToken cancellationToken)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

        return order.OrderId;
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .SingleOrDefaultAsync(order => order.OrderId == orderId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> CheckOrderIdExists(int orderId, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .AnyAsync(order => order.OrderId == orderId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Order?> MarkOrderAsPaidAsync(int orderId)
    {
        var order = await _dbContext.Orders
            .SingleOrDefaultAsync(order => order.OrderId == orderId)
            .ConfigureAwait(false);

        if (order == null)
        {
            return null;
        }

        order.Paid = true;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return order;
    }

    public async Task<Order?> MarkOrderAsShippedAsync(int orderId)
    {
        var order = await _dbContext.Orders
            .SingleOrDefaultAsync(order => order.OrderId == orderId)
            .ConfigureAwait(false);

        if (order == null)
        {
            return null;
        }

        order.Shipped = true;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return order;
    }
}