using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Application.Dtos;
using OrderProcessing.Application.Handlers.Commands;
using OrderProcessing.Application.Handlers.Queries;

namespace OrderProcessing.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
		{
			var command = new CreateOrderCommand { Order = orderDto };
			var orderId = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetOrder), new { orderNumber = orderId }, orderId);
		}

		[HttpGet("{orderNumber}")]
		public async Task<IActionResult> GetOrder(Guid orderNumber)
		{
			var query = new GetOrderByIdQuery { Id = orderNumber };
			var order = await _mediator.Send(query);
			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}
	}
}