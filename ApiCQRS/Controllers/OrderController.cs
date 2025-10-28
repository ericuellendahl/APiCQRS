using ApiCQRS.Aplication.DTOs;
using ApiCQRS.Aplication.UseCase.Order.Commands;
using ApiCQRS.Aplication.UseCase.Order.Queries;
using ApiCQRS.Domian.Interfaces.Orders;
using Microsoft.AspNetCore.Mvc;

namespace ApiCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IQueryHandler<GetOrderByIdQuery, OrderDto?> _getOrderQueryHandler;
        private readonly ICommandHandler<CreateOrderCommand, OrderDto> _createOrderCommandHandler;

        public OrderController(ILogger<OrderController> logger,
            IQueryHandler<GetOrderByIdQuery, OrderDto?> getOrderQueryHandler,
            ICommandHandler<CreateOrderCommand, OrderDto> createOrderCommandHandler)
        {
            _logger = logger;
            _getOrderQueryHandler = getOrderQueryHandler;
            _createOrderCommandHandler = createOrderCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand order)
        {
            if (order is null)
            {
                _logger.LogWarning("Received null order in CreateOrder");
                return BadRequest("Order cannot be null");
            }

            var createCommand = await _createOrderCommandHandler.HandleAsync(order);

            return createCommand is null
                ? BadRequest("Failed to create order")
                : CreatedAtAction(nameof(GetOrder), new { id = createCommand.Id }, order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _getOrderQueryHandler.HandleAsync(new GetOrderByIdQuery(id));

            if (order == null)
            {
                _logger.LogWarning("Order with ID: {OrderId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Retrieved {Id} orders", order.Id);

            return Ok(order);
        }
    }
}
