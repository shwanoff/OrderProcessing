namespace OrderProcessing.Domain
{
    public struct Invoice
	{
		public required string Address { get; set; }
		public required string Email { get; set; }
		public required string CreditCard { get; set; }

		public override readonly int GetHashCode()
		{
			return HashCode.Combine(
				Address.ToUpperInvariant(),
				Email.ToUpperInvariant(),
				CreditCard.ToUpperInvariant()
			);
		}

		public override readonly bool Equals(object? obj)
		{
			if (obj is Invoice other)
			{
				if (GetHashCode() != other.GetHashCode())
				{
					return false;
				}

				return Address.Equals(other.Address, StringComparison.InvariantCultureIgnoreCase) &&
					   Email.Equals(other.Email, StringComparison.InvariantCultureIgnoreCase) &&
					   CreditCard.Equals(other.CreditCard, StringComparison.InvariantCultureIgnoreCase);
			}
			return false;
		}

		public static bool operator ==(Invoice left, Invoice right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Invoice left, Invoice right)
		{
			return !(left.Equals(right));
		}

		public override readonly string ToString()
		{
			return Email;
		}
		
	}
}
