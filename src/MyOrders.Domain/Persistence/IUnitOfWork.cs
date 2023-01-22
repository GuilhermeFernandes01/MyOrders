namespace MyOrders.Domain.Persistence
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);
    }
}