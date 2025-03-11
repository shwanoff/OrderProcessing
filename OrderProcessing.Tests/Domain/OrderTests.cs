using OrderProcessing.Domain;

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
				new() { Name = "Laptop", Amount = 2, Price = 1500.00m },
				new() { Name = "Mouse", Amount = 1, Price = 25.00m }
			};
			var invoice = new Invoice
			{
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234-5678-9101-1121"
			};
			var createdAt = DateTime.UtcNow;

			// Act
			var order = new Order
			{
				Id = 1,
				Products = products,
				Invoice = invoice,
				CreatedAt = createdAt
			};

			// Assert
			Assert.That(order.Id, Is.EqualTo(1));
			Assert.That(order.Products, Is.EqualTo(products));
			Assert.That(order.Invoice, Is.EqualTo(invoice));
			Assert.That(order.CreatedAt, Is.EqualTo(createdAt));
		}

		[Test]
		public void Order_ToString_ShouldReturnFormattedString()
		{
			// Arrange
			var products = new List<Product>
			{
				new() { Name = "Laptop", Amount = 2, Price = 1500.00m },
				new() { Name = "Mouse", Amount = 1, Price = 25.00m }
			};
			var invoice = new Invoice
			{
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234-5678-9101-1121"
			};
			var createdAt = new DateTime(2025, 3, 11, 10, 0, 0);
			var order = new Order
			{
				Id = 1,
				Products = products,
				Invoice = invoice,
				CreatedAt = createdAt
			};

			// Act
			var result = order.ToString();

			// Assert
			Assert.That(result, Is.EqualTo("#1 2025-03-11 10:00:00"));
		}
	}
}