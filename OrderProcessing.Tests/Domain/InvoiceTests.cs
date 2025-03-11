using OrderProcessing.Domain;

namespace OrderProcessing.Tests.Domain
{
	[TestFixture]
	public class InvoiceTests
	{
		[Test]
		public void Invoice_Equals_ShouldReturnTrueForEqualInvoices()
		{
			// Arrange
			var invoice1 = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };
			var invoice2 = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };

			// Act
			var result = invoice1.Equals(invoice2);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Invoice_Equals_ShouldReturnFalseForDifferentInvoices()
		{
			// Arrange
			var invoice1 = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };
			var invoice2 = new Invoice { Address = "456 Elm St", Email = "test2@example.com", CreditCard = "1111-2222-3333-4444" };

			// Act
			var result = invoice1.Equals(invoice2);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Invoice_GetHashCode_ShouldReturnSameHashCodeForEqualInvoices()
		{
			// Arrange
			var invoice1 = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };
			var invoice2 = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };

			// Act
			var hashCode1 = invoice1.GetHashCode();
			var hashCode2 = invoice2.GetHashCode();

			// Assert
			Assert.That(hashCode1, Is.EqualTo(hashCode2));
		}

		[Test]
		public void Invoice_GetHashCode_ShouldReturnDifferentHashCodeForDifferentInvoices()
		{
			// Arrange
			var invoice1 = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };
			var invoice2 = new Invoice { Address = "456 Elm St", Email = "test2@example.com", CreditCard = "1111-2222-3333-4444" };

			// Act
			var hashCode1 = invoice1.GetHashCode();
			var hashCode2 = invoice2.GetHashCode();

			// Assert
			Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
		}

		[Test]
		public void Invoice_ToString_ShouldReturnEmail()
		{
			// Arrange
			var invoice = new Invoice { Address = "123 Main St", Email = "test@example.com", CreditCard = "1234-5678-9101-1121" };

			// Act
			var result = invoice.ToString();

			// Assert
			Assert.That(result, Is.EqualTo("test@example.com"));
		}
	}
}