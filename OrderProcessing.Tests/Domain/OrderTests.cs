using OrderProcessing.Domain;
using NUnit.Framework;

namespace OrderProcessing.Tests.Domain
{
	[TestFixture]
	public class OrderTests
	{
		[Test]
		public void Order_Constructor_ShouldInitializeProperties()
		{
			// Arrange
			var products = new List<Product>
			{
				new("Laptop", 2, 1500.00m),
				new("Mouse", 1, 25.00m)
			};
			var address = "123 Main St";
			var email = "test@example.com";
			var creditCard = "1234-5678-9101-1121";

			// Act
			var order = new Order
			{
				Products = products,
				Address = address,
				Email = email,
				CreditCard = creditCard
			};

			// Assert
			Assert.That(order.Products, Is.EqualTo(products));
			Assert.That(order.Address, Is.EqualTo(address));
			Assert.That(order.Email, Is.EqualTo(email));
			Assert.That(order.CreditCard, Is.EqualTo(creditCard));
			Assert.That(order.Id, Is.Not.EqualTo(Guid.Empty));
			Assert.That(order.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(1).Seconds);
		}

		[Test]
		public void Order_ToString_ShouldReturnFormattedString()
		{
			// Arrange
			var products = new List<Product>
			{
				new("Laptop", 2, 1500.00m),
				new("Mouse", 1, 25.00m)
			};
			var address = "123 Main St";
			var email = "test@example.com";
			var creditCard = "1234-5678-9101-1121";
			var createdAt = new DateTime(2025, 3, 11, 10, 0, 0);
			var id = Guid.NewGuid();
			var order = new Order
			{
				Id = id,
				Products = products,
				Address = address,
				Email = email,
				CreditCard = creditCard,
				CreatedAt = createdAt
			};

			// Act
			var result = order.ToString();

			// Assert
			Assert.That(result, Is.EqualTo($"#{order.Id} {email} 2025-03-11 10:00:00"));
		}
	}
}