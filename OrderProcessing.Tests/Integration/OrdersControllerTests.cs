using System.Net;
using System.Net.Http.Json;
using OrderProcessing.Application.Dtos;

namespace OrderProcessing.Tests.Integration
{
	[TestFixture]
	public class OrdersControllerTests
	{
		private IntegrationTestsWebApplicationFactory _factory;
		private HttpClient _client;

		[SetUp]
		public void SetUp()
		{
			_factory = new IntegrationTestsWebApplicationFactory();
			_client = _factory.CreateClient();
		}

		[Test]
		public async Task CreateOrder_ReturnsCreatedOrder()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Sample Street, 90402 Berlin",
				InvoiceEmailAddress = "customer@example.com",
				InvoiceCreditCardNumber = "1234-5678-9101-1121",
				Products =
				[
					new ProductDto
					{
						ProductId = 12345,
						ProductName = "Gaming Laptop",
						ProductAmount = 2,
						ProductPrice = 1499.99m
					}
				]
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public async Task GetOrder_ReturnsOrder()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Sample Street, 90402 Berlin",
				InvoiceEmailAddress = "customer@example.com",
				InvoiceCreditCardNumber = "1234-5678-9101-1121",
				Products =
				[
					new ProductDto
					{
						ProductId = 12345,
						ProductName = "Gaming Laptop",
						ProductAmount = 2,
						ProductPrice = 1499.99m
					}
				]
			};

			var createResponse = await _client.PostAsJsonAsync("/api/orders", orderDto);
			createResponse.EnsureSuccessStatusCode();
			var orderId = await createResponse.Content.ReadFromJsonAsync<Guid>();

			// Act
			var response = await _client.GetAsync($"/api/orders/{orderId}");

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			var returnedOrder = await response.Content.ReadFromJsonAsync<OrderDto>();
			Assert.That(returnedOrder, Is.Not.Null);
			Assert.That(returnedOrder.OrderNumber, Is.EqualTo(orderId));
		}
	}
}