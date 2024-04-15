
using AutoMapper;
using MediatR;
using Ordering.Application.Common.Mappings;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Order.Commands.CreateCommand
{
    public class CreateOrderCommand : IRequest<ApiResult<long>>, IMapFrom<Domain.Entities.Order>
    {

        public string UserName { get; set; }

        public decimal ToatalPrice { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string ShippingAddress { get; set; }

        public string InvoiceAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommand, Domain.Entities.Order>();
        }
    }
}
