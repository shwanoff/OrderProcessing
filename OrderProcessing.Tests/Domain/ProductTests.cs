using OrderProcessing.Domain;

namespace OrderProcessing.Tests.Domain
{
	[TestFixture]
	public class ProductTests
	{
		[Test]
		public void Product_Constructor_ShouldInitializeProperties()
		{
			// Arrange
			var id = 1u;
			var name = "Laptop";
			var amount = 2u;
			var price = 1500.00m;

			// Act
			var product = new Product(id, name, amount, price);

			// Assert
			Assert.That(product.Name, Is.EqualTo(name));
			Assert.That(product.Amount, Is.EqualTo(amount));
			Assert.That(product.Price, Is.EqualTo(price));
		}

		[Test]
		public void Product_Equals_ShouldReturnTrueForEqualProducts()
		{
			// Arrange
			var product1 = new Product(1, "Laptop", 2, 1500.00m);
			var product2 = new Product(1, "Laptop", 2, 1500.00m);

			// Act
			var result = product1.Equals(product2);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Product_Equals_ShouldReturnFalseForDifferentProducts()
		{
			// Arrange
			var product1 = new Product(1, "Laptop", 2, 1500.00m);
			var product2 = new Product(2, "Desktop", 1, 1200.00m);

			// Act
			var result = product1.Equals(product2);

			// Assert
			Assert.That(result, Is.False);
		}
	}
}