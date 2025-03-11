using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Entities;

namespace OrderProcessing.Tests.Infrastructure
{
	[TestFixture]
	public class OrderRepositoryTests
	{
		private DbContextOptions<OrderDbContext> _dbContextOptions;
		private IMapper _mapper;
		private OrderDbContext _context;
		private OrderRepository _repository;

		[SetUp]
		public void SetUp()
		{
			var dbName = Guid.NewGuid().ToString();
			_dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>()
				.UseInMemoryDatabase(databaseName: dbName)
				.Options;

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});
			_mapper = config.CreateMapper();

			_context = new OrderDbContext(_dbContextOptions);
			_repository = new OrderRepository(_context, _mapper);
		}

		[TearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[Test]
		public async Task AddAsync_ShouldAddOrderAndReturnId()
		{
			// Arrange
			var order = new Order
			{
				Id = Guid.NewGuid(),
				Products =
				[
					new Product 
					{ 
						Id = 1, 
						Name = "Laptop", 
						Amount = 2, 
						Price = 1500.00m 
					},
					new Product 
					{ 
						Id = 2, 
						Name = "Mouse", 
						Amount = 1, 
						Price = 25.00m 
					}
				],
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				CreatedAt = DateTime.UtcNow
			};

			// Act
			var result = await _repository.AddAsync(order);

			// Assert
			Assert.That(result, Is.EqualTo(order.Id));
			var orderEntity = await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == order.Id);
			Assert.That(orderEntity, Is.Not.Null);
			Assert.That(orderEntity.Id, Is.EqualTo(order.Id));
			Assert.That(orderEntity.Address, Is.EqualTo(order.Address));
			Assert.That(orderEntity.Email, Is.EqualTo(order.Email));
			Assert.That(orderEntity.CreditCard, Is.EqualTo(order.CreditCard));
			Assert.That(orderEntity.CreatedAt, Is.EqualTo(order.CreatedAt));
			Assert.That(orderEntity.Products.Count, Is.EqualTo(order.Products.Count));
		}

		[Test]
		public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
		{
			// Arrange
			var orderEntity = new OrderEntity
			{
				Id = Guid.NewGuid(),
				Products =
				[
					new ProductEntity 
					{ 
						Id = 1, 
						Name = "Laptop", 
						Amount = 2, 
						Price = 1500.00m 
					},
					new ProductEntity 
					{ 
						Id = 2, 
						Name = "Mouse", 
						Amount = 1, 
						Price = 25.00m 
					}
				],
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				CreatedAt = DateTime.UtcNow
			};

			await _context.Orders.AddAsync(orderEntity);
			await _context.SaveChangesAsync();

			// Act
			var result = await _repository.GetByIdAsync(orderEntity.Id);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(orderEntity.Id));
			Assert.That(result.Address, Is.EqualTo(orderEntity.Address));
			Assert.That(result.Email, Is.EqualTo(orderEntity.Email));
			Assert.That(result.CreditCard, Is.EqualTo(orderEntity.CreditCard));
			Assert.That(result.CreatedAt, Is.EqualTo(orderEntity.CreatedAt));
			Assert.That(result.Products.Count, Is.EqualTo(orderEntity.Products.Count));
		}

		[Test]
		public async Task GetByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
		{
			// Arrange
			var nonExistentOrderId = Guid.NewGuid();

			// Act
			var result = await _repository.GetByIdAsync(nonExistentOrderId);

			// Assert
			Assert.That(result, Is.Null);
		}
	}
}