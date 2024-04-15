
using Contracts.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepositoryBaseAsync<Order,long>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);

        Task CreateOrder(Order order);

        Task UpdateOrder(string userName ,Order order);

        Task DeleteOrder(string userName);
    }
}
