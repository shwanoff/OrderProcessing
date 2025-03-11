namespace OrderProcessing.Domain
{
    public struct Product
	{
		public required string Name { get; set; }
		public required uint Amount { get; set; }
		public required decimal Price { get; set; }

		public override readonly int GetHashCode()
		{
			return HashCode.Combine(
				Name.ToUpperInvariant(),
				Amount,
				Price
			);
		}

		public override readonly bool Equals(object? obj)
		{
			if (obj is Product other)
			{
				if (GetHashCode() != other.GetHashCode())
				{
					return false;
				}
				return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase) &&
					   Amount == other.Amount &&
					   Price == other.Price;
			}
			return false;
		}

		public static bool operator ==(Product left, Product right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Product left, Product right)
		{
			return !(left.Equals(right));
		}

		public override readonly string ToString()
		{
			return Name;
		}
	}
}
