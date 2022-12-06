using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System.Linq.Expressions;

using (StoreContext context = new StoreContext())
{
    var customer = context.Customers
                    .Where(c => c.FirstName == "Eric")
                    .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderDetails)
                    .ThenInclude(od => od.Products)
                    .FirstOrDefault();

    Console.WriteLine($"Customer");
    Console.WriteLine($"First Name:\t{customer?.FirstName ?? "-"}");
    Console.WriteLine($"Last Name:\t{customer?.LastName ?? "-"}");
    Console.WriteLine($"Address:\t{customer?.Address ?? "-"}");

    if (customer?.Orders.Any() == true)
    {
        foreach (Order order in customer.Orders)
        {
            Console.WriteLine("Order");
            Console.WriteLine($"Order Placed:\t{order?.OrderPlaced.ToString() ?? "-"}");
            Console.WriteLine($"Order Fulfilled:\t{order?.OrderFulfilled.ToString() ?? "-"}");

            if (order?.OrderDetails.Any() == true) {
                foreach (OrderDetail orderDetail in order.OrderDetails) {
                    Console.WriteLine($"Qty:\t{orderDetail.Quantity}");
                    Console.WriteLine($"Name:\t{orderDetail.Products.Name}");
                    Console.WriteLine($"Price:\t{orderDetail.Products.Price}");
                }
            }
        }
    }

}

