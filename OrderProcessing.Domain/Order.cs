namespace OrderProcessing.Domain
{
	public class Order
	{
		public required uint Id { get; set; }
		public required List<Product> Products { get; set; }
		public required Invoice Invoice { get; set; }
		public required DateTime CreatedAt { get; set; }

		public override string ToString()
		{
			return $"#{Id} {CreatedAt:yyyy-MM-dd HH:mm:ss}";
		}
	}
}
