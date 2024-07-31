

namespace Order_Management_System.Controllers
{

    public class InvoicesController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoicesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //GET /api/invoices/{invoiceId} - Get details of a specific invoice(admin only)

        [Authorize(Roles = "Admin")]
        [HttpGet("{invoiceId}")]
        [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int invoiceId)
        {
            var invoice = await _unitOfWork.Repository<Invoice>().GetAsync(invoiceId);
            if (invoice is null)
                return NotFound();

            return Ok(_mapper.Map<InvoiceDto>(invoice));
        }


        //GET /api/invoices - Get all invoices(admin only)

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetAllInvoices()
        {

            var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
            if (invoices is null)
                return NotFound();

            return Ok(_mapper.Map<IReadOnlyList<InvoiceDto>>(invoices));
        }

    }

}

