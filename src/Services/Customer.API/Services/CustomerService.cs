using AutoMapper;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Services
{
    public class CustomerService :ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IResult> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Entities.Customer>(createCustomerDto);
            await _customerRepository.CreateCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
            var result = _mapper.Map<CustomerDto>(customer);
            return Results.Ok(result);
        }
        public async Task<IResult> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();     
            var result = _mapper.Map<List<CustomerDto>>(customers);
            return Results.Ok(result);
        }

        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            var customer = await _customerRepository.GetCustomerbyUserNameAsync(userName);
            if (customer == null) return Results.NotFound();
            var result = _mapper.Map<CustomerDto>(customer);
            return Results.Ok(result);
        }

        public async Task<IResult> UpdateCustomerAsync(int id ,UpdateCustomerDto updateCustomerDto)
        {
           var customerExist = await _customerRepository.GetCustomerbyIdAsync(id);
            if(customerExist != null)
            {
                var newCustomer = _mapper.Map(updateCustomerDto, customerExist);
                await _customerRepository.UpdateCustomerAsync(newCustomer);
                await _customerRepository.SaveChangesAsync();
                var result = _mapper.Map<CustomerDto>(newCustomer);
                return Results.Ok(result);
            }


            return Results.NotFound();
        }

        public async Task<IResult> DeleteCustomerAsync(int id)
        {
            var customerExist = await _customerRepository.GetCustomerbyIdAsync(id);
            if (customerExist != null)
            {              
                await _customerRepository.DeleteCustommerByIdAsync(id);
                await _customerRepository.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
    }
}
