using Basket.API.Entities;
using Basket.API.Repository.Interface;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repository
{
    public class BasketRepository: IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly ILogger _logger;
        public BasketRepository(IDistributedCache redisCache, ILogger logger)
        {
            _redisCache = redisCache;
            _logger = logger;
        }
        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            _logger.Information($"BEGIN: Getting basket for {userName}");
            var basket = await _redisCache.GetStringAsync(userName);
            _logger.Information($"END: Basket for {userName} : {basket}");

            return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {

            var basketString = JsonSerializer.Serialize(cart);

            _logger.Information($"BEGIN: Updating basket for {cart.UserName} : {basketString}");

            if (options != null)         
                await _redisCache.SetStringAsync(cart.UserName, basketString, options);
            else
                await _redisCache.SetStringAsync(cart.UserName, basketString);

            _logger.Information($"END: Basket updated for {cart.UserName} : {basketString}");
       
            return await GetBasketByUserName(cart.UserName);
        }

        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await _redisCache.RemoveAsync(userName);
                return true;
            }
            catch(Exception ex)
            {
               _logger.Error($"Error deleting basket for {userName} : {ex.Message}");
                throw;
            }
            
        }
        
       
    }
}
