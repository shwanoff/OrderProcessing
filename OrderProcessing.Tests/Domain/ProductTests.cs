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
			var name = "Laptop";
			var amount = 2u;
			var price = 1500.00m;

			// Act
			var product = new Product(name, amount, price);

			// Assert
			Assert.That(product.Name, Is.EqualTo(name));
			Assert.That(product.Amount, Is.EqualTo(amount));
			Assert.That(product.Price, Is.EqualTo(price));
		}

		[Test]
		public void Product_GetHashCode_ShouldReturnSameHashCodeForEqualProducts()
		{
			// Arrange
			var product1 = new Product("Laptop", 2, 1500.00m);
			var product2 = new Product("Laptop", 2, 1500.00m);

			// Act
			var hashCode1 = product1.GetHashCode();
			var hashCode2 = product2.GetHashCode();

			// Assert
			Assert.That(hashCode1, Is.EqualTo(hashCode2));
		}

		[Test]
		public void Product_GetHashCode_ShouldReturnDifferentHashCodeForDifferentProducts()
		{
			// Arrange
			var product1 = new Product("Laptop", 2, 1500.00m);
			var product2 = new Product("Desktop", 1, 1200.00m);

			// Act
			var hashCode1 = product1.GetHashCode();
			var hashCode2 = product2.GetHashCode();

			// Assert
			Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
		}

		[Test]
		public void Product_ToString_ShouldReturnName()
		{
			// Arrange
			var name = "Laptop";
			var product = new Product(name, 2, 1500.00m);

			// Act
			var result = product.ToString();

			// Assert
			Assert.That(result, Is.EqualTo(name));
		}
	}
}