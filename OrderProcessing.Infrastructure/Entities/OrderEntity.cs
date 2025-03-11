namespace OrderProcessing.Infrastructure.Entities
{
    public class OrderEntity
    {
		public Guid Id { get; set; }
		public List<ProductEntity> Products { get; set; } = new();
		public string Address { get; set; }
		public string Email { get; set; }
		public string CreditCard { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
