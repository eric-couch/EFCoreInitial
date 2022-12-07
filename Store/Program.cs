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
    }
} while (quitOrder);