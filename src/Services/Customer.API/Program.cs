using AutoMapper;
using Common.Logging;
using Contracts.Common;
using Customer.API;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.DTOs.Customer;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Customer API up");

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                   .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
                   .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                   .AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
    var app = builder.Build();

    //api enponit
    app.MapGet("/", () => "Welcome to customer minimal api");

    app.MapGet("api/customers",
        async (ICustomerService customerService) => await customerService.GetCustomersAsync());

    app.MapGet("api/customers/{userName}", async (string userName, ICustomerService customerService) => await customerService.GetCustomerByUserNameAsync(userName));


    app.MapPost("api/customer/create-customer", async (CreateCustomerDto createCustomerDto, ICustomerService customerService) => await customerService.CreateCustomerAsync(createCustomerDto));

    app.MapPut("api/customer/update-customer/{id}", async (int id,UpdateCustomerDto updateCustomerDto, ICustomerService customerService) => await customerService.UpdateCustomerAsync(id,updateCustomerDto));

    app.MapDelete("api/customer/delete-customer/{id}", async (int id, ICustomerService customerService) => await customerService.DeleteCustomerAsync(id));

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedCustomerData().Run();

}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled Exception: {ex.Message}");
}
finally
{
    Log.Information("Shut down Customer API complete");
    Log.CloseAndFlush();
}
