using AutoMapper;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Entities;

namespace OrderProcessing.Tests.Infrastructure
{
	[TestFixture]
	public class MappingProfileTests
	{
		private IMapper _mapper;

		[SetUp]
		public void SetUp()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});
			_mapper = config.CreateMapper();
		}

		[Test]
		public void Should_Map_Order_To_OrderDto()
		{
			// Arrange
			var order = new Order
			{
				Id = Guid.NewGuid(),
				Products =
				[
					new Product 
					( 
						Id: 1, 
						Name: "Laptop", 
						Amount: 2, 
						Price: 1500.00m 
					),
					new Product 
					(
						Id: 2, 
						Name: "Mouse", 
						Amount: 1, 
						Price: 25.00m
					)
				],
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				CreatedAt = DateTime.UtcNow
			};

			// Act
			var orderDto = _mapper.Map<OrderDto>(order);

			// Assert
			Assert.That(orderDto, Is.Not.Null);
			Assert.That(orderDto.OrderNumber, Is.EqualTo(order.Id));
			Assert.That(orderDto.InvoiceAddress, Is.EqualTo(order.Address));
			Assert.That(orderDto.InvoiceEmailAddress, Is.EqualTo(order.Email));
			Assert.That(orderDto.InvoiceCreditCardNumber, Is.EqualTo(order.CreditCard));
			Assert.That(orderDto.CreatedAt, Is.EqualTo(order.CreatedAt));
			Assert.That(orderDto.Products.Count, Is.EqualTo(order.Products.Count));
		}

		[Test]
		public void Should_Map_OrderDto_To_Order()
		{
			// Arrange
			var orderDto = new OrderDto
			(
				OrderNumber: Guid.NewGuid(),
				Products:
				[
					new ProductDto 
					(
						ProductId: 1,
						ProductName: "Laptop", 
						ProductAmount: 2, 
						ProductPrice: 1500.00m
					),
					new ProductDto 
					(
						ProductId: 2,
						ProductName: "Mouse",
						ProductAmount: 1,
						ProductPrice: 25.00m
					)
				],
				InvoiceAddress: "123 Main St",
				InvoiceEmailAddress: "test@example.com",
				InvoiceCreditCardNumber: "1234567890123452",
				CreatedAt: DateTime.UtcNow
			);

			// Act
			var order = _mapper.Map<Order>(orderDto);

			// Assert
			Assert.That(order, Is.Not.Null);
			Assert.That(order.Id, Is.EqualTo(orderDto.OrderNumber));
			Assert.That(order.Address, Is.EqualTo(orderDto.InvoiceAddress));
			Assert.That(order.Email, Is.EqualTo(orderDto.InvoiceEmailAddress));
			Assert.That(order.CreditCard, Is.EqualTo(orderDto.InvoiceCreditCardNumber));
			Assert.That(order.CreatedAt, Is.EqualTo(orderDto.CreatedAt));
			Assert.That(order.Products.Count, Is.EqualTo(orderDto.Products.Count));
		}

		[Test]
		public void Should_Map_Product_To_ProductDto()
		{
			// Arrange
			var product = new Product
			(
				Id: 1,
				Name: "Laptop",
				Amount: 2,
				Price: 1500.00m
			);

			// Act
			var productDto = _mapper.Map<ProductDto>(product);

			// Assert
			Assert.That(productDto, Is.Not.Null);
			Assert.That(productDto.ProductId, Is.EqualTo(product.Id));
			Assert.That(productDto.ProductName, Is.EqualTo(product.Name));
			Assert.That(productDto.ProductAmount, Is.EqualTo(product.Amount));
			Assert.That(productDto.ProductPrice, Is.EqualTo(product.Price));
		}

		[Test]
		public void Should_Map_ProductDto_To_Product()
		{
			// Arrange
			var productDto = new ProductDto
			(
				ProductId: 1,
				ProductName: "Laptop",
				ProductAmount: 2,
				ProductPrice: 1500.00m
			);

			// Act
			var product = _mapper.Map<Product>(productDto);

			// Assert
			Assert.That(product, Is.Not.Null);
			Assert.That(product.Id, Is.EqualTo(productDto.ProductId));
			Assert.That(product.Name, Is.EqualTo(productDto.ProductName));
			Assert.That(product.Amount, Is.EqualTo(productDto.ProductAmount));
			Assert.That(product.Price, Is.EqualTo(productDto.ProductPrice));
		}

		[Test]
		public void Should_Map_Order_To_OrderEntity()
		{
			// Arrange
			var order = new Order
			{
				Id = Guid.NewGuid(),
				Products =
				[
					new Product 
					( 
						Id: 1, 
						Name: "Laptop", 
						Amount: 2, 
						Price: 1500.00m 
					),
					new Product 
					( 
						Id: 2, 
						Name: "Mouse", 
						Amount: 1, 
						Price: 25.00m 
					)
				],
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				CreatedAt = DateTime.UtcNow
			};

			// Act
			var orderEntity = _mapper.Map<OrderEntity>(order);

			// Assert
			Assert.That(orderEntity, Is.Not.Null);
			Assert.That(orderEntity.Id, Is.EqualTo(order.Id));
			Assert.That(orderEntity.Address, Is.EqualTo(order.Address));
			Assert.That(orderEntity.Email, Is.EqualTo(order.Email));
			Assert.That(orderEntity.CreditCard, Is.EqualTo(order.CreditCard));
			Assert.That(orderEntity.CreatedAt, Is.EqualTo(order.CreatedAt));
			Assert.That(orderEntity.Products.Count, Is.EqualTo(order.Products.Count));
		}

		[Test]
		public void Should_Map_OrderEntity_To_Order()
		{
			// Arrange
			var orderEntity = new OrderEntity
			{
				Id = Guid.NewGuid(),
				Products = new List<ProductEntity>
				{
					new ProductEntity { Id = 1, Name = "Laptop", Amount = 2, Price = 1500.00m },
					new ProductEntity { Id = 2, Name = "Mouse", Amount = 1, Price = 25.00m }
				},
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				CreatedAt = DateTime.UtcNow
			};

			// Act
			var order = _mapper.Map<Order>(orderEntity);

			// Assert
			Assert.That(order, Is.Not.Null);
			Assert.That(order.Id, Is.EqualTo(orderEntity.Id));
			Assert.That(order.Address, Is.EqualTo(orderEntity.Address));
			Assert.That(order.Email, Is.EqualTo(orderEntity.Email));
			Assert.That(order.CreditCard, Is.EqualTo(orderEntity.CreditCard));
			Assert.That(order.CreatedAt, Is.EqualTo(orderEntity.CreatedAt));
			Assert.That(order.Products.Count, Is.EqualTo(orderEntity.Products.Count));
		}

		[Test]
		public void Should_Map_Product_To_ProductEntity()
		{
			// Arrange
			var product = new Product
			(
				Id: 1,
				Name: "Laptop",
				Amount: 2,
				Price: 1500.00m
			);

			// Act
			var productEntity = _mapper.Map<ProductEntity>(product);

			// Assert
			Assert.That(productEntity, Is.Not.Null);
			Assert.That(productEntity.Id, Is.EqualTo(product.Id));
			Assert.That(productEntity.Name, Is.EqualTo(product.Name));
			Assert.That(productEntity.Amount, Is.EqualTo(product.Amount));
			Assert.That(productEntity.Price, Is.EqualTo(product.Price));
		}

		[Test]
		public void Should_Map_ProductEntity_To_Product()
		{
			// Arrange
			var productEntity = new ProductEntity
			{
				Id = 1,
				Name = "Laptop",
				Amount = 2,
				Price = 1500.00m
			};

			// Act
			var product = _mapper.Map<Product>(productEntity);

			// Assert
			Assert.That(product, Is.Not.Null);
			Assert.That(product.Id, Is.EqualTo(productEntity.Id));
			Assert.That(product.Name, Is.EqualTo(productEntity.Name));
			Assert.That(product.Amount, Is.EqualTo(productEntity.Amount));
			Assert.That(product.Price, Is.EqualTo(productEntity.Price));
		}
	}
}