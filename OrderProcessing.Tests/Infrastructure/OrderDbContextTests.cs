using Microsoft.EntityFrameworkCore;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Entities;

namespace OrderProcessing.Tests.Infrastructure
{
	[TestFixture]
	public class OrderDbContextTests
	{
		private DbContextOptions<OrderDbContext> _dbContextOptions;

		[SetUp]
		public void SetUp()
		{
			var dbName = Guid.NewGuid().ToString();
			_dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>()
				.UseInMemoryDatabase(databaseName: dbName)
				.Options;
		}

		[Test]
		public async Task CanAddAndRetrieveOrder()
		{
			// Arrange
			var order = new OrderEntity
			{
				Id = Guid.NewGuid(),
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				CreatedAt = DateTime.UtcNow,
				Products =
				[
					new ProductEntity 
					{ 
						Id = Guid.NewGuid(),
						ProductId = 1,
						Name = "Laptop", 
						Amount = 2, 
						Price = 1500.00m 
					},
					new ProductEntity 
					{ 
						Id = Guid.NewGuid(),
						ProductId = 2, 
						Name = "Mouse", 
						Amount = 1, 
						Price = 25.00m 
					}
				]
			};

			// Act
			using (var context = new OrderDbContext(_dbContextOptions))
			{
				await context.Orders.AddAsync(order);
				await context.SaveChangesAsync();
			}

			// Assert
			using (var context = new OrderDbContext(_dbContextOptions))
			{
				var retrievedOrder = await context.Orders
					.Include(o => o.Products)
					.FirstOrDefaultAsync(o => o.Id == order.Id);

				Assert.That(retrievedOrder, Is.Not.Null);
				Assert.That(retrievedOrder.Id, Is.EqualTo(order.Id));
				Assert.That(retrievedOrder.Address, Is.EqualTo(order.Address));
				Assert.That(retrievedOrder.Email, Is.EqualTo(order.Email));
				Assert.That(retrievedOrder.CreditCard, Is.EqualTo(order.CreditCard));
				Assert.That(retrievedOrder.CreatedAt, Is.EqualTo(order.CreatedAt));
				Assert.That(retrievedOrder.Products.Count, Is.EqualTo(order.Products.Count));
			}
		}
	}
}