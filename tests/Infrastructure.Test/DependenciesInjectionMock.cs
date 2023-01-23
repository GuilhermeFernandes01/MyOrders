using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyOrders.Domain.Models;
using MyOrders.Infrastructure;

namespace Infrastructure.Test
{
	public static class DependenciesInjectionMock
	{
		public static MyOrdersDbContext Build()
		{
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

            DbContextOptions<MyOrdersDbContext> options = new DbContextOptionsBuilder<MyOrdersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var dbContext = new MyOrdersDbContext(options);
            dbContext.Database.EnsureCreated();

            dbContext.Set<Order>().AddRange(
                new Order { ProductName = "productTest1", Quantity = 10 },
                new Order { ProductName = "productTest2", Quantity = 1 },
                new Order { ProductName = "productTest1", Quantity = 99 }
            );

            dbContext.SaveChanges();

            return dbContext;
        }

        public static UnitOfWork Build(MyOrdersDbContext context)
        {
            return new UnitOfWork(context);
        }
	}
}

