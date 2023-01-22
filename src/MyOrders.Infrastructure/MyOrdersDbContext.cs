using MyOrders.Domain.Models;
using MyOrders.Domain.Persistence;
using MyOrders.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MyOrders.Infrastructure
{
    public class MyOrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public MyOrdersDbContext(DbContextOptions<MyOrdersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderEntityConfiguration).Assembly);
        }
    }
}