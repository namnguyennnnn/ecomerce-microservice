using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Order.Commands.CreateCommand
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IValidator<CreateOrderCommand> _validator;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CreateOrderCommandHandler> logger, IValidator<CreateOrderCommand> validator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"BEGIN: Handling CreateOrderCommand with UserName {request.UserName}... ");
                var newOrder = _mapper.Map<Domain.Entities.Order>(request);
                await _orderRepository.CreateOrder(newOrder);
                await _orderRepository.SaveChangesAsync();
                _logger.LogInformation("END: CreateOrderCommand handled successfully.");
                return new ApiSuccessResult<long>(newOrder.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling CreateOrderCommand.");
                return new ApiErrorResult<long>("An error occurred while processing the request.");
            }
        }
    }
}
