using QueryLoadingEntityFramework;
using QueryLoadingEntityFramework.Entities;

ApplicationDbContext context = new ApplicationDbContext();

var query = context.Customers
        .Select(c => new
        {
            Name = c.FirstName,
            Surname = c.LastName,
            Orders = c.Orders.Where(p => p.CustomerId == c.CustomerId).Select(o => new Order()
            {
                OrderDate = o.OrderDate,
                OrderId = o.OrderId
            }),
            OrderDetails = c.Orders.SelectMany(o => o.OrderDetails).Select(p => new OrderDetail()
            {
                ProductName = p.ProductName,
                Price = p.Price
            })
        });

// Show the result
foreach (var item in query)
{
    Console.WriteLine($"Customer: {item.Name} {item.Surname}");

    foreach (var order in item.Orders)
        Console.WriteLine($"{order.OrderId} {order.OrderDate}");

    foreach (OrderDetail orderDetail in item.OrderDetails)
    {
        Console.WriteLine($"\t {orderDetail.ProductName} {orderDetail.Price}");
    }
    Console.WriteLine();
}

Console.WriteLine("\nDone!");