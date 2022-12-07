using Microsoft.EntityFrameworkCore;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data
{
    public class StoreContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=sqlserver;Initial Catalog=Store;User ID=ssis;Password=fullhouse;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                Id = 1,
                FirstName = "Eric",
                LastName = "Couch",
                Address = "201 Shaffner St.",
                Email = "eric.couch@cognizant.com",
                Phone = "(817) 304-9048"
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                Name = "Meat Lover's Pizza",
                Price = 9.99M
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 2,
                Name = "Deluxe Pizza",
                Price = 12.99M
            });
            modelBuilder.Entity<Order>().HasData(new Order()
            {
                Id = 1,
                OrderPlaced = new DateTime(2022, 12, 6, 10, 30, 00),
                OrderFulfilled = new DateTime(2022, 12, 6, 10, 45, 00),
                CustomerId = 1
            });
            modelBuilder.Entity<OrderDetail>().HasData(new OrderDetail()
            {
                Id = 1,
                Quantity = 1,
                ProductId = 1,
                OrderId = 1
            });
            modelBuilder.Entity<Order>().HasData(new Order()
            {
                Id = 2,
                OrderPlaced = new DateTime(2022, 12, 5, 11, 30, 00),
                OrderFulfilled = new DateTime(2022, 12, 5, 11, 50, 00),
                CustomerId = 1
            });
            modelBuilder.Entity<OrderDetail>().HasData(new OrderDetail()
            {
                Id = 2,
                Quantity = 1,
                ProductId = 2,
                OrderId = 2
            });
        }
    }
}
