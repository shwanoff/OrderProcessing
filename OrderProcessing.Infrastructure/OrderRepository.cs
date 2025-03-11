using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure.Entities;

namespace OrderProcessing.Infrastructure
{
	public class OrderRepository(OrderDbContext context, IMapper mapper) : IOrderRepository
	{
		private readonly OrderDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task<Guid> AddAsync(Order order)
		{
			var orderEntity = _mapper.Map<OrderEntity>(order);

			await _context.Orders.AddAsync(orderEntity);
			await _context.SaveChangesAsync();

			return orderEntity.Id;
		}

		public async Task<Order?> GetByIdAsync(Guid id)
		{
			var orderEntity = await _context.Orders
				.Include(o => o.Products)
				.FirstOrDefaultAsync(o => o.Id == id);

			return _mapper.Map<Order?>(orderEntity);
		}
	}
}
