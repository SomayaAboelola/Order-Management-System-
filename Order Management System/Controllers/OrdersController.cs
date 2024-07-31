


namespace Order_Management_System.Controllers
{

    public class OrdersController : BaseAPIController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderServices, IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto input)
        {
            var order = await _orderServices.CreateOrderAsync(input);
            return Ok(order);
        }

        // GET /api/orders/{orderId } - Get details of a specific order

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResultDto>> GetOrderById(int orderId)
        {
            var order = await _orderServices.GetOrderForUserById(orderId);
            if (order is null)
                return NotFound();
            return Ok(order);
        }

        //GET /api/orders - Get all orders(admin only)

        [Authorize(Roles= "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(OrderResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrders()
        {
            var order = await _orderServices.GetOrderForUser();
            return Ok(order);
        }

        //PUT /api/orders/{orderId}/ status - Update order status(admin only)

        [Authorize(Roles= "Admin")]
        [HttpPut("{orderId}/status")]
        [ProducesResponseType(typeof(OrderResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<OrderResultDto>> UpdateOrderStatus(int orderId, [FromBody] OrderDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var getOrder = await _orderServices.GetOrderForUserById(orderId);

            if (getOrder is null)
                return NotFound();

            try
            {
                var order = await _orderServices.UpdateOrderAsync(getOrder.Id, input);
                return Ok(order);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error updating product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update product");
            }
        }



    }
}