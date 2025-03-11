namespace OrderProcessing.Domain
{
    public record Product(string name, uint amount, decimal price)
	{
		public string Name { get; } = name;
		public uint Amount { get; } = amount;
		public decimal Price { get; } = price;

		public override int GetHashCode()
		{
			return HashCode.Combine(
				Name.ToUpperInvariant(),
				Amount,
				Price
			);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
