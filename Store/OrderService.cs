using Store.Data;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderService
    {
        private readonly StoreContext _context;

        public OrderService(StoreContext context)
        {
            _context = context;
        }

        public Customer? FindCustomer(Customer cust)
        {
            var foundCustomer = (from customer in _context.Customers
                                 where customer.FirstName == cust.FirstName
                                 where customer.LastName == cust.LastName
                                 select customer).FirstOrDefault();
            return foundCustomer;
        }

        public List<Order> GetOrders(Customer cust)
        {
            List<Order> orders = (from c in _context.Customers
                                  join o in _context.Orders on c.Id equals o.CustomerId
                                  where c.FirstName == cust.FirstName
                                  where c.LastName == cust.LastName
                                  select o).ToList();
            return orders;
        }

        public string ListOrder(List<Order> orders)
        {
            string ret = "";
            foreach (var o in orders)
            {
                List<OrderDetail> details = (from od in _context.OrderDetails
                                             where od.OrderId == o.Id
                                             select od).ToList();
                foreach (OrderDetail od in details)
                {
                    ret += $"Order Placed: {o.OrderPlaced}\n";
                    Product product = (from p in _context.Products
                                       where p.Id == od.ProductId
                                       select p).FirstOrDefault()!;
                    ret += $"""
                        Qty:    {od.Quantity}
                            Product:    {product.Name}
                            Price:      {product.Price}

                        """;
                }
            }
            return ret;
        }

        public Product? GetProduct(int id)
        {
            var product = (from p in _context.Products
                           where p.Id == id
                           select p).FirstOrDefault();
            return product;
        }

        public void SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void SaveOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }
        public static string MainMenu()
        {
            return """
                (L)ist Order History
                (P)lace Order
                (Q)uit
                """;
        }
        public string OrderMenu()
        {
            string ret = "";
            var products = (from p in _context.Products
                            select p);
            ret += "Select Item by number:\n";
            foreach (var p in products)
            {
                ret += $"({p.Id}) {p.Name} {p.Price}\n";
            }
            ret += "(Q)uit\n";
            return ret;
        }
    }
}
