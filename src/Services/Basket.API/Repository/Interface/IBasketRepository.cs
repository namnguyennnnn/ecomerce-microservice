using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repository.Interface
{
    public interface IBasketRepository
    {
        Task<Cart?> GetBasketByUserName(string userName);
        Task<Cart> UpdateBasket(Cart basket,DistributedCacheEntryOptions options = null);
        Task<bool> DeleteBasketFromUserName(string userName);
    }
}
