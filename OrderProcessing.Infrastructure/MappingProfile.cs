using AutoMapper;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure.Entities;

namespace OrderProcessing.Infrastructure
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.InvoiceAddress, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.InvoiceEmailAddress, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.InvoiceCreditCardNumber, opt => opt.MapFrom(src => src.CreditCard.FormatCreditCard()))
				.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
				.ReverseMap()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderNumber ?? Guid.NewGuid()))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.InvoiceAddress))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.InvoiceEmailAddress))
				.ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => src.InvoiceCreditCardNumber.RemoveCreditCardFormatting()))
				.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt ?? DateTime.UtcNow));

			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.ProductAmount, opt => opt.MapFrom(src => src.Amount))
				.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
				.ReverseMap()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
				.ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.ProductAmount))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice));

			CreateMap<Order, OrderEntity>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => src.CreditCard))
				.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
				.ReverseMap()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => src.CreditCard))
				.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

			CreateMap<Product, ProductEntity>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ReverseMap()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
		}
	}
}
