
using Contracts.Common;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderReposiory :RepositoryBaseAsync<Order, long, OrderContext>, IOrderRepository
    {
        public OrderReposiory(OrderContext dbcontext , IUnitOfWork<OrderContext> unitOfWork) : base(dbcontext, unitOfWork)
        {

        }

        public async Task CreateOrder(Order order) => await CreateAsync(order);

        public async Task DeleteOrder(string userName)
        {
            var existingOrder = await FindByCondition(x => x.UserName.Equals(userName)).FirstOrDefaultAsync();
            if(existingOrder != null)
                await DeleteAsync(existingOrder);
        } 

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) =>
            await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();

        public async Task UpdateOrder(string userName , Order order)
        {
            var existingOrder = await FindByCondition(x => x.UserName.Equals(userName)).FirstOrDefaultAsync();
            if (existingOrder != null) await UpdateAsync(order);
                        
        }
    }
}
