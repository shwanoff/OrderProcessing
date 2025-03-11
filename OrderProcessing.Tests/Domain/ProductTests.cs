using OrderProcessing.Domain;

namespace OrderProcessing.Tests.Domain
{
	[TestFixture]
	public class ProductTests
	{
		[Test]
		public void Product_Equals_ShouldReturnTrueForEqualProducts()
		{
			// Arrange
			var product1 = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };
			var product2 = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };

			// Act
			var result = product1.Equals(product2);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void Product_Equals_ShouldReturnFalseForDifferentProducts()
		{
			// Arrange
			var product1 = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };
			var product2 = new Product { Name = "Desktop", Amount = 1, Price = 1200.00m };

			// Act
			var result = product1.Equals(product2);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void Product_GetHashCode_ShouldReturnSameHashCodeForEqualProducts()
		{
			// Arrange
			var product1 = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };
			var product2 = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };

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
			var product1 = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };
			var product2 = new Product { Name = "Desktop", Amount = 1, Price = 1200.00m };

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
			var product = new Product { Name = "Laptop", Amount = 2, Price = 1500.00m };

			// Act
			var result = product.ToString();

			// Assert
			Assert.That(result, Is.EqualTo("Laptop"));
		}
	}
}