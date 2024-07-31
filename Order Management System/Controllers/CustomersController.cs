

namespace Order_Management_System.Controllers
{

    public class CustomersController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerServices _customerServices;

        public CustomersController(IUnitOfWork unitOfWork ,ICustomerServices customerServices)
        {
            _unitOfWork = unitOfWork;
            _customerServices = customerServices;
        }

        //POST /api/customers - Create a new customer

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerDto customer)
        {
            if (customer is null)
                return BadRequest();

         await  _customerServices.CreateCustomerAsync(customer);
           

            return Ok(customer);

        }

        //GET /api/customers/{customerId}/orders - Get all orders for a customer

        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<OrderResultDto>> GetOrderForCustomer(int customerId)
        {
          return await _customerServices.GetCustomerOrderAsync(customerId);    
        }



    }
}
