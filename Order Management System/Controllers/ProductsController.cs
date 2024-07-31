
global using Orders.Repository._Data;

namespace Order_Management_System.Controllers
{

    public class ProductsController : BaseAPIController
    {
       
        private readonly IProductServices _productServices;
        private readonly OrderDbContext _context;

        public ProductsController(IProductServices productServices, OrderDbContext context)
        {
            _productServices = productServices;
            _context = context;
        }

        
        // GET /api/products - Get all products

        [HttpGet]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
             var products = await _productServices.GetAllProductsAsync();
              if (products is null)
                    return NotFound();
        
            return Ok(products);
        }

        // GET /api/products/{productId} - Get details of a specific product
      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productServices.GetProductById(id);
            if (product is null)
                return NotFound();  
         return Ok(product);
        }

        // POST /api/products - Add a new product(admin only)

        [Authorize(Roles="Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct (Product input)
        {
            var product = await _productServices.CreateProduct(input);
            if (product is null)
                return BadRequest(new ResponseApi(400));
            return Ok(product); 
        }

        // PUT /api/products/{productId} .Update product details(admin only)

        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> UpdateProductAsync(int productId , [FromBody] Product input)
        {
            var productServices =_productServices;
            if(!await _productServices.ProductExistsAsync(productId))
                return NotFound();
            try
            {
                await productServices.UpdateProduct(productId, input);
                return input;  
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Error updating product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update product");
            }
        }
    }
}
