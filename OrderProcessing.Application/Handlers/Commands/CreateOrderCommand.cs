using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain;

namespace OrderProcessing.Application.Handlers.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
	{
		public required OrderDto Order { get; set; }
	}

	public class CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CreateOrderCommandHandler> logger) : IRequestHandler<CreateOrderCommand, Guid>
	{
		private readonly IOrderRepository _orderRepository = orderRepository;
		private readonly IMapper _mapper = mapper;
		private readonly ILogger<CreateOrderCommandHandler> _logger = logger;

		public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var order = _mapper.Map<Order>(request);
				await _orderRepository.AddAsync(order);
				
				_logger.LogInformation("Order created: {OrderId}", order.Id);
				
				return order.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating order");
				throw;
			}
		}
	}

	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x => x.Order.InvoiceAddress).NotEmpty().WithMessage("Invoice address is required");
			RuleFor(x => x.Order.InvoiceEmailAddress).EmailAddress().WithMessage("Invalid email address");
			RuleFor(x => x.Order.InvoiceCreditCardNumber).CreditCard().WithMessage("Invalid credit card number");
			RuleForEach(x => x.Order.Products).ChildRules(products =>
			{
				products.RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product name is required");
				products.RuleFor(p => p.ProductAmount).GreaterThan(0).WithMessage("Product amount must be greater than 0");
				products.RuleFor(p => p.ProductPrice).GreaterThanOrEqualTo(0).WithMessage("Product price must be greater than or equal to 0");
			});
		}
	}
}
