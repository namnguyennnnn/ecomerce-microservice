using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        private readonly ILogger _logger;
        private readonly OrderContext _context;

        public OrderContextSeed(OrderContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if(_context.Database.IsSqlServer())
                    await _context.Database.MigrateAsync();
            }
            catch(Exception ex)
            {
                _logger.Error(ex,"An error occur while initialize database");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.Error(ex,"An error occur while seeding database");
                throw;
            }
        }

        public async Task TrySeedAsync() 
        {            
            if(!_context.Orders.Any())
            {
               await _context.Orders.AddRangeAsync(
                   new Order
                   {
                       UserName = "customer1",
                       FirstName = "Sam",
                       LastName = "Nasr",
                       EmailAddress = "customer1@local.com",
                       ToatalPrice = 350,
                       ShippingAddress = "Shipping Address",
                       InvoiceAddress = "Invoice Address"
                   });

            }
        }
    }
}
