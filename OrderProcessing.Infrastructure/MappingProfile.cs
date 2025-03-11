using AutoMapper;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Domain;

namespace OrderProcessing.Infrastructure
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderDto>().ReverseMap();
			CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}
