using Basket.API.Entities;
using Basket.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketsController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get-basket/{userName}")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> GetBasket(string userName)
        {
            var result = await _repository.GetBasketByUserName(userName);
            return Ok(result ?? new Cart(userName));
        }

        [HttpPost("update-basket")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart cart)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
            var result = await _repository.UpdateBasket(cart,options);
            return Ok(result);
        }

        [HttpDelete("delete-basket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            var result = await _repository.DeleteBasketFromUserName(userName);
            return Ok(result);
        }
    }
}
