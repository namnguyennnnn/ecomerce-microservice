using Contracts.Common;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
    {
        Task<Entities.Customer> GetCustomerbyUserNameAsync(string userName);
        Task<Entities.Customer> GetCustomerbyIdAsync(int id);

        Task<IEnumerable<Entities.Customer>> GetCustomersAsync();

        Task CreateCustomerAsync(Entities.Customer customer);

        Task UpdateCustomerAsync(Entities.Customer customer);

        Task DeleteCustommerByIdAsync(int id);
    }
}
