using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Controllers
{
    public static class CustomersController
    {
        public static void MapCustomersAPI(this WebApplication app)
        {
            app.MapGet("/", () => "Welcome to customer minimal api");

            app.MapGet("api/customers",
                async (ICustomerService customerService) => await customerService.GetCustomersAsync());

            app.MapGet("api/customers/{userName}", async (string userName, ICustomerService customerService) => await customerService.GetCustomerByUserNameAsync(userName));


            app.MapPost("api/customer/create-customer", async (CreateCustomerDto createCustomerDto, ICustomerService customerService) => await customerService.CreateCustomerAsync(createCustomerDto));

            app.MapPut("api/customer/update-customer/{id}", async (int id, UpdateCustomerDto updateCustomerDto, ICustomerService customerService) => await customerService.UpdateCustomerAsync(id, updateCustomerDto));

            app.MapDelete("api/customer/delete-customer/{id}", async (int id, ICustomerService customerService) => await customerService.DeleteCustomerAsync(id));
        }
    }
}
