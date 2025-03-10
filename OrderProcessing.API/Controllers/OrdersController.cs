using Microsoft.AspNetCore.Mvc;

namespace OrderProcessing.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		[HttpPost]
		public IActionResult CreateOrder()
		{
			// TODO: Implement order creation
			return Ok("CreateOrder endpoint");
		}

		[HttpGet("{orderNumber}")]
		public IActionResult GetOrder(string orderNumber)
		{
			// TODO: Implement order retrieval
			return Ok($"GetOrder endpoint for order number: {orderNumber}");
		}
	}
}