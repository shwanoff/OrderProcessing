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
		public async Task CreateOrder_ReturnsCreatedOrderId()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "1234567890123452",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = 2,
						ProductPrice = 1500.00m
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
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "1234567890123452",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = 2,
						ProductPrice = 1500.00m
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

		[Test]
		public async Task CreateOrder_WithInvalidEmail_ReturnsBadRequest()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "invalid-email",
				InvoiceCreditCardNumber = "1234567890123452",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = 2,
						ProductPrice = 1500.00m
					}
				]
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task CreateOrder_WithEmptyInvoiceAddress_ReturnsBadRequest()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "1234567890123452",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = 2,
						ProductPrice = 1500.00m
					}
				]
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task CreateOrder_WithInvalidCreditCardNumber_ReturnsBadRequest()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "invalid-credit-card",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = 2,
						ProductPrice = 1500.00m
					}
				]
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task CreateOrder_WithEmptyProductsList_ReturnsBadRequest()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "1234567890123452",
				Products = new List<ProductDto>()
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task CreateOrder_WithNegativeProductAmount_ReturnsBadRequest()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "1234567890123452",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = -1,
						ProductPrice = 1500.00m
					}
				]
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public async Task CreateOrder_WithNegativeProductPrice_ReturnsBadRequest()
		{
			// Arrange
			var orderDto = new OrderDto
			{
				InvoiceAddress = "123 Main St",
				InvoiceEmailAddress = "test@example.com",
				InvoiceCreditCardNumber = "1234567890123452",
				Products =
				[
					new ProductDto
					{
						ProductId = 1,
						ProductName = "Laptop",
						ProductAmount = 2,
						ProductPrice = -10.00m
					}
				]
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/orders", orderDto);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}
	}
}