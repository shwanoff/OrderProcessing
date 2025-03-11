using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Application.Handlers.Queries;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain;

namespace OrderProcessing.Tests.Application
{
	[TestFixture]
	public class GetOrderByIdQueryTests
	{
		private Mock<IOrderRepository> _orderRepositoryMock;
		private Mock<IMapper> _mapperMock;
		private Mock<ILogger<GetOrderByIdQueryHandler>> _loggerMock;
		private GetOrderByIdQueryHandler _handler;

		[SetUp]
		public void SetUp()
		{
			_orderRepositoryMock = new Mock<IOrderRepository>();
			_mapperMock = new Mock<IMapper>();
			_loggerMock = new Mock<ILogger<GetOrderByIdQueryHandler>>();
			_handler = new GetOrderByIdQueryHandler(_orderRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
		}

		[Test]
		public async Task Handle_ShouldReturnOrderDto_WhenOrderIsFound()
		{
			// Arrange
			var orderId = Guid.NewGuid();
			var createdAt = DateTime.UtcNow;
			var order = new Order
			{
				Id = orderId,
				Products = [],
				Address = "123 Main St",
				Email = "test@example.com",
				CreditCard = "1234-5678-9101-1121",
				CreatedAt = createdAt
			};

			var orderDto = new OrderDto
			(
				OrderNumber: orderId, 
				InvoiceAddress: "123 Main St", 
				InvoiceEmailAddress: "test@example.com", 
				InvoiceCreditCardNumber: "1234-5678-9101-1121", 
				Products: [], 
				CreatedAt: createdAt
			);

			_orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);
			_mapperMock.Setup(mapper => mapper.Map<OrderDto>(order)).Returns(orderDto);

			var query = new GetOrderByIdQuery { Id = orderId };

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.That(result, Is.EqualTo(orderDto));
		}

		[Test]
		public async Task Handle_ShouldReturnNull_WhenOrderIsNotFound()
		{
			// Arrange
			var orderId = Guid.NewGuid();
			_orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync((Order?)null);

			var query = new GetOrderByIdQuery { Id = orderId };

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.That(result, Is.Null);
		}
	}
}