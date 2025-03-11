using AutoMapper;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Moq;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Application.Handlers.Commands;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain;

namespace OrderProcessing.Tests.Application
{
	[TestFixture]
	public class CreateOrderCommandTests
	{
		private Mock<IOrderRepository> _orderRepositoryMock;
		private Mock<IMapper> _mapperMock;
		private Mock<ILogger<CreateOrderCommandHandler>> _loggerMock;
		private CreateOrderCommandHandler _handler;
		private CreateOrderCommandValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_orderRepositoryMock = new Mock<IOrderRepository>();
			_mapperMock = new Mock<IMapper>();
			_loggerMock = new Mock<ILogger<CreateOrderCommandHandler>>();
			_handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
			_validator = new CreateOrderCommandValidator();
		}

		[Test]
		public async Task Handle_ShouldReturnOrderId_WhenOrderIsCreated()
		{
			// Arrange
			var orderId = Guid.NewGuid();
			var createdAt = DateTime.UtcNow;
			var orderDto = new OrderDto
			(
				OrderNumber: orderId,
				InvoiceAddress: "123 Main St",
				InvoiceEmailAddress: "test@example.com",
				InvoiceCreditCardNumber: "1234567890123452",
				Products: 
				[
					new ProductDto
					(
						ProductId: 1,
						ProductName: "Laptop",
						ProductAmount: 2,
						ProductPrice: 1500.00m
					)
				],
				CreatedAt: createdAt
			);
			var order = new Order
			{
				Id = orderId,
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234567890123452",
				Products = 
				[
					new Product
					(
						ProductId: 1,
						Name: "Laptop",
						Amount: 2,
						Price: 1500.00m
					)
				],
				CreatedAt = createdAt
			};

			_mapperMock.Setup(mapper => mapper.Map<Order>(It.IsAny<OrderDto>())).Returns(order);
			_orderRepositoryMock.Setup(repo => repo.AddAsync(order)).ReturnsAsync(orderId);

			var command = new CreateOrderCommand { Order = orderDto };

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.That(result, Is.EqualTo(orderId));
		}

		[Test]
		public void Should_PassValidation_WhenDataIsValid()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "123 Main St",
					InvoiceEmailAddress: "test@example.com",
					InvoiceCreditCardNumber: "1234567890123452",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "Laptop", 
							ProductAmount: 2,
							ProductPrice: 1500.00m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);

			// Assert
			result.ShouldNotHaveAnyValidationErrors();
		}

		[Test]
		public void Should_FailValidation_WhenInvoiceAddressIsEmpty()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "",
					InvoiceEmailAddress: "test@example.com",
					InvoiceCreditCardNumber: "1234567890123452",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "Laptop",
							ProductAmount: 2,
							ProductPrice: 1500.00m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Order.InvoiceAddress);
		}

		[Test]
		public void Should_FailValidation_WhenInvoiceEmailAddressIsInvalid()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "123 Main St",
					InvoiceEmailAddress: "invalid-email",
					InvoiceCreditCardNumber: "1234567890123452",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "Laptop",
							ProductAmount: 2,
							ProductPrice: 1500.00m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Order.InvoiceEmailAddress);
		}

		[Test]
		public void Should_FailValidation_WhenInvoiceCreditCardNumberIsInvalid()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "123 Main St",
					InvoiceEmailAddress: "test@example.com",
					InvoiceCreditCardNumber: "invalid-credit-card",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "Laptop",
							ProductAmount: 2,
							ProductPrice: 1500.00m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Order.InvoiceCreditCardNumber);
		}

		[Test]
		public void Should_FailValidation_WhenProductNameIsEmpty()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "123 Main St",
					InvoiceEmailAddress: "test@example.com",
					InvoiceCreditCardNumber: "1234567890123452",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "",
							ProductAmount: 2,
							ProductPrice: 1500.00m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);

			// TODO: There is an issue in FluentValidation library
			// ShouldHaveValidationErrorFor method doesn't designed to validate properties of properties
			// So, I'm validating using regular Assert

			// Assert
			Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Order.Products[0].ProductName"));
		}

		[Test]
		public void Should_FailValidation_WhenProductAmountIsLessThanOrEqualToZero()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "123 Main St",
					InvoiceEmailAddress: "test@example.com",
					InvoiceCreditCardNumber: "1234567890123452",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "Laptop",
							ProductAmount: 0,
							ProductPrice: 1500.00m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);


			// TODO: There is an issue in FluentValidation library
			// ShouldHaveValidationErrorFor method doesn't designed to validate properties of properties
			// So, I'm validating using regular Assert

			// Assert
			Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Order.Products[0].ProductAmount"));
		}

		[Test]
		public void Should_FailValidation_WhenProductPriceIsLessThanZero()
		{
			// Arrange
			var command = new CreateOrderCommand
			{
				Order = new OrderDto
				(
					InvoiceAddress: "123 Main St",
					InvoiceEmailAddress: "test@example.com",
					InvoiceCreditCardNumber: "1234567890123452",
					Products:
					[
						new ProductDto
						(
							ProductId: 1,
							ProductName: "Laptop",
							ProductAmount: 2,
							ProductPrice: -1m
						)
					]
				)
			};

			// Act
			var result = _validator.TestValidate(command);

			// TODO: There is an issue in FluentValidation library
			// ShouldHaveValidationErrorFor method doesn't designed to validate properties of properties
			// So, I'm validating using regular Assert

			// Assert
			Assert.That(result.Errors[0].PropertyName, Is.EqualTo("Order.Products[0].ProductPrice"));
		}
	}
}