using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "xandao", FirstName = "Alexandre", LastName = "Romero", EmailAddress = "romeroleonardoalexandre@gmail.com", AddressLine = "mina nova", Country = "Brasil", TotalPrice = 350, CVV = "123456", CardName = "asdas", CardNumber = "12154654", Expiration = "02/23", 
                    LastModifiedBy = "", CreatedBy = "", CreatedDate= new DateTime(), LastModifiedDate= new DateTime(), PaymentMethod = 1, State = "SC", ZipCode = "9854511"
                }
            };
        }
    }
}