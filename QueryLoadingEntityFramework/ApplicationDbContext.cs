using Microsoft.EntityFrameworkCore;
using QueryLoadingEntityFramework.Entities;

namespace QueryLoadingEntityFramework
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyWorkerDb;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                        .HasOne(o => o.Customer)
                        .WithMany(c => c.Orders)
                        .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                        .HasOne(od => od.Order)
                        .WithMany(o => o.OrderDetails)
                        .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order>().HasData
            (
                new Order { OrderId = 1, OrderDate = DateTime.Now.AddDays(-5), CustomerId = 1 },
                new Order { OrderId = 2, OrderDate = DateTime.Now.AddDays(-4), CustomerId = 2 },
                new Order { OrderId = 3, OrderDate = DateTime.Now.AddDays(-3), CustomerId = 3 },
                new Order { OrderId = 4, OrderDate = DateTime.Now.AddDays(-2), CustomerId = 4 },
                new Order { OrderId = 5, OrderDate = DateTime.Now.AddDays(-1), CustomerId = 5 },
                new Order { OrderId = 6, OrderDate = DateTime.Now.AddDays(-5), CustomerId = 1 },
                new Order { OrderId = 7, OrderDate = DateTime.Now.AddDays(-4), CustomerId = 2 },
                new Order { OrderId = 8, OrderDate = DateTime.Now.AddDays(-3), CustomerId = 2 },
                new Order { OrderId = 9, OrderDate = DateTime.Now.AddDays(-2), CustomerId = 4 },
                new Order { OrderId = 10, OrderDate = DateTime.Now.AddDays(-1), CustomerId = 3 }
            );

            modelBuilder.Entity<OrderDetail>().HasData
            (
                new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductName = "Product1", Price = 10.5m },
                new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductName = "Product2", Price = 20.5m },
                new OrderDetail { OrderDetailId = 3, OrderId = 2, ProductName = "Product3", Price = 30.5m },
                new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductName = "Product4", Price = 40.5m },
                new OrderDetail { OrderDetailId = 5, OrderId = 3, ProductName = "Product5", Price = 50.5m },
                new OrderDetail { OrderDetailId = 6, OrderId = 4, ProductName = "Product6", Price = 60.5m },
                new OrderDetail { OrderDetailId = 7, OrderId = 5, ProductName = "Product7", Price = 70.5m },
                new OrderDetail { OrderDetailId = 8, OrderId = 2, ProductName = "Product8", Price = 65.5m },
                new OrderDetail { OrderDetailId = 9, OrderId = 2, ProductName = "Product9", Price = 40.5m },
                new OrderDetail { OrderDetailId = 10, OrderId = 3, ProductName = "Product10", Price = 54.5m },
                new OrderDetail { OrderDetailId = 11, OrderId = 4, ProductName = "Product11", Price = 78.5m },
                new OrderDetail { OrderDetailId = 12, OrderId = 5, ProductName = "Product12", Price = 70.5m }
            );

            modelBuilder.Entity<Customer>().HasData
            (
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" },
                new Customer { CustomerId = 3, FirstName = "Emily", LastName = "Johnson", Email = "emily.johnson@example.com" },
                new Customer { CustomerId = 4, FirstName = "William", LastName = "Brown", Email = "william.brown@example.com" },
                new Customer { CustomerId = 5, FirstName = "Sophia", LastName = "Williams", Email = "sophia.williams@example.com" }
            );
        }
    }
}
