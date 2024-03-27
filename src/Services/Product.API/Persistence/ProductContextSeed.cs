using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext,ILogger logger )
        {
            if(!productContext.Products.Any())
            {
                productContext.AddRange(getCatalogProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seeded data for Product DB associated with context {DbContextName}", nameof(ProductContext));
            }
        }

        private static IEnumerable<CatalogProduct> getCatalogProducts()
        {
            return new List<CatalogProduct>() 
            {
                new()
                {
                    No = "Lotus",
                    Name = "Esprit",
                    Summary = "This is sumary for this product",
                    Description ="This is sumary for this product",
                    Price = (decimal)177940.49
                },
                new()
                {
                    No = "Ducati",
                    Name = "Ducati 1000",
                    Summary = "This is sumary for this product",
                    Description ="This is sumary for this product",
                    Price = (decimal)177940.49
                }
            };
        }
    }
}
