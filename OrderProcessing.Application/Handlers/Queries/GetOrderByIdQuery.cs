using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Application.Interfaces;

namespace OrderProcessing.Application.Handlers.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto?>
	{
		public required Guid Id { get; set; }
	}

	public class GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrderByIdQueryHandler> logger) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
	{
		private readonly IOrderRepository _orderRepository = orderRepository;
		private readonly IMapper _mapper = mapper;
		private readonly ILogger _logger = logger;

		public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var order = await _orderRepository.GetByIdAsync(request.Id);
				
				if (order == null)
				{
					_logger.LogWarning("Order not found with ID: {OrderId}", request.Id);
					return null;
				}

				var orderDto = _mapper.Map<OrderDto>(order);
				
				_logger.LogInformation("Order retrieved: {OrderId}", request.Id);
				
				return orderDto;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving order with ID: {OrderId}", request.Id);
				throw;
			}
		}
	}
}
