namespace OrderProcessing.Domain
{
	public class Order
	{
		public Guid Id { get; init; } = Guid.NewGuid();
		public required IReadOnlyList<Product> Products { get; set; }
		public required string Address { get; set; }
		public required string Email { get; set; }
		public required string CreditCard { get; set; }
		public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

		public override string ToString()
		{
			return $"#{Id} {Email} {CreatedAt:yyyy-MM-dd HH:mm:ss}";
		}
	}
}
