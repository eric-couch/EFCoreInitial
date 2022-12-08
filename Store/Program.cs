//Add functionality to our model to take orders from the user via the console.  
//    Have the user enter their customer data or select their account via their 
//    first name and last name.  Allow the user to place orders.​

using Store;
using Store.Data;
using Store.Models;

Customer ThisCustomer = new();
StoreContext context = new();
OrderService ThisOrder = new(context);

Console.Write("Enter First Name: ");
ThisCustomer.FirstName = Console.ReadLine() ?? "";
Console.Write("Enter Last Name: ");
ThisCustomer.LastName = Console.ReadLine() ?? "";

var findCustomer = ThisOrder.FindCustomer(ThisCustomer);

if (findCustomer == null)
{
    Console.WriteLine("Customer Not Found.");
    Console.Write("Enter Address:");
    ThisCustomer.Address = Console.ReadLine() ?? "";
    Console.Write("Enter Phone:");
    ThisCustomer.Phone = Console.ReadLine() ?? "";
    Console.Write("Enter Email:");
    ThisCustomer.Email = Console.ReadLine() ?? "";
}
else
{
    ThisCustomer = findCustomer;
    Console.WriteLine("Customer record found.");
    Console.WriteLine(ThisCustomer.ToString());
}

bool quitOrder = false;
// Main Order Loop
do
{
    Console.WriteLine(OrderService.MainMenu());
    string userResponse = Console.ReadLine() ?? "";

    if (userResponse.ToLower() == "l")
    {
        Console.Clear();
        List<Order> custOrders = ThisOrder.GetOrders(ThisCustomer);
        if (custOrders is null)
        {
            Console.WriteLine("No orders found.");
        } else
        {
            Console.WriteLine(ThisOrder.ListOrder(custOrders));
        }
    } else if (userResponse.ToLower() == "p")
    {
        Console.Clear();
        Order newOrder = new Order()
        {
            Customer = ThisCustomer,
            OrderPlaced = DateTime.Now
        };
        OrderDetail od = new OrderDetail()
        {
            Orders = newOrder
        };
        bool donewithProducts = false;
        do
        {
            Console.WriteLine(ThisOrder.OrderMenu());
            string orderItem = Console.ReadLine() ?? "q";
            if (orderItem.ToLower() == "q")
            {
                donewithProducts = true;
                break;
            }  else if (Int32.TryParse(orderItem, out int productNumber))
            {
                var product = ThisOrder.GetProduct(productNumber);
                if (product is not null)
                {
                    od.Products = product;
                    od.Quantity = 1;
                    ThisOrder.SaveOrder(newOrder);
                    ThisOrder.SaveOrderDetail(od);
                }
            } else
            {
                Console.WriteLine("Invalid entry. Try again.");
            }

        } while (!donewithProducts);
    } else if (userResponse.ToLower() == "q")
    {
        quitOrder = true;
    } else
    {
        Console.WriteLine("Invalid Entry. Try again.");
    }
} while (!quitOrder);