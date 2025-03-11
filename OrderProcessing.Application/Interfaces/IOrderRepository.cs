using OrderProcessing.Domain;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderRepository
    {
		Task<Guid> AddAsync(Order order);
		Task<Order?> GetByIdAsync(Guid id);
	}
}
