using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Order.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrdersQueryHandler> _logger;

        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrdersQueryHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"BEGIN: Handling GetOrdersQuery with UserName {request.UserName}... ");

                var orders = await _orderRepository.GetOrdersByUserName(request.UserName);

                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                _logger.LogInformation("END: GetOrdersQuery handled successfully.");

                return new ApiSuccessResult<List<OrderDto>>(orderDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling GetOrdersQuery.");
                return new ApiErrorResult<List<OrderDto>>("An error occurred while processing the request.");
            }
        }
    }
}
