using MyOrders.Domain.Persistence;

namespace MyOrders.Infrastructure
{
	public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
		private readonly MyOrdersDbContext _myOrdersDbContext;
		private bool _disposed;

		public UnitOfWork(MyOrdersDbContext myOrdersDbContext)
		{
			_myOrdersDbContext = myOrdersDbContext;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            var result = await _myOrdersDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public void Dispose()
		{
			Dispose(true);
		}

        private void Dispose(bool disposing)
		{
			if (!_disposed && disposing)
			{
				_myOrdersDbContext.Dispose();
			}

			_disposed = true;
		}
	}
}

