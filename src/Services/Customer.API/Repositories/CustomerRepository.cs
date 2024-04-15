using Contracts.Common;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;


namespace Customer.API.Repositories
{
    public class CustomerRepository: RepositoryBaseAsync<Entities.Customer,int,CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext , IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext , unitOfWork)
        {

        }

        public Task CreateCustomerAsync(Entities.Customer customer) => CreateAsync(customer);

        public async Task DeleteCustommerByIdAsync(int id)
        {
            var customerExist = await FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
            if (customerExist != null) await DeleteAsync(customerExist);
        }

        public Task<Entities.Customer> GetCustomerbyIdAsync(int id) =>
            FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();

        public Task<Entities.Customer> GetCustomerbyUserNameAsync(string userName) => 
            FindByCondition(u => u.UserName.Equals(userName)).SingleOrDefaultAsync();

        public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync() => await FindAll().ToListAsync();

        public Task UpdateCustomerAsync(Entities.Customer customer) => UpdateAsync(customer);
    }
}
