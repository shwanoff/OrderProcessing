namespace OrderProcessing.Infrastructure.Entities
{
    public class ProductEntity
    {
		public uint Id { get; set; }
		public string Name { get; set; }
		public uint Amount { get; set; }
		public decimal Price { get; set; }

		public Guid OrderId { get; set; }
		public OrderEntity Order { get; set; }
	}
}
