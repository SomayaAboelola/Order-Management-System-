
using Orders.Core.Service.Contract;
using Order = Orders.Core.Entities.Order;
namespace Orders.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderValidationServices _validateOrder;
        private readonly EmailService _emailServices;



        public OrderServices(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOrderValidationServices validateOrder,
            EmailService emailServices)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validateOrder = validateOrder;
            _emailServices = emailServices;

        }
     
        #region Create Order
        public async Task<OrderResultDto?> CreateOrderAsync(OrderDto input)
        {

            //1.check validation

            if (!await _validateOrder.ValidateOrder(input))
                throw new InvalidOperationException("Order validation failed.");


            //2.Get Selected Item

            var OrderItems = new List<OrderItem>();
            foreach (var item in input.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.ProductId);
                var orderItem = new OrderItem(product.Id, product.Price, item.Quantity);
                OrderItems.Add(orderItem);
            }

            //  3.Calculate Total Amount
            var totalAmount = ((double)input.Items.Sum(item => item.UnitPrice * item.Quantity));

            //  4.Apply discount

            var discountedAmount = totalAmount;
            if (totalAmount > 200)
            {
                discountedAmount *= 0.9; // 10% discount
            }
            else if (totalAmount > 100)
            {
                discountedAmount *= 0.95; // 5% discount
            }


            //5.Update product stock

            foreach (var item in input.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.ProductId);
                product.Stock -= item.Quantity;
                _unitOfWork.Repository<Product>().Update(product);

            }

        
            //6. Generate invoice

            var invoice = new Invoice
            {
                OrderId = input.Id,
                InvoiceDate = DateTimeOffset.UtcNow,
                TotalAmount = totalAmount ,
                Order=_mapper.Map<Order>(input),
            };
            await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
            // 7. Map From OrderItemDto => OrderITem 
            var mappedItems = _mapper.Map<List<OrderItem>>(OrderItems);

            //8.Create Oder 
            var order = new Order()
            {
                CustomerId = input.CustomerId,
                OrderDate = DateTimeOffset.UtcNow,
                orderStatus = OrderStatus.Pending,
                Items = mappedItems,
                TotalAmount = totalAmount,
                PriceWithDiscount = discountedAmount,
            };


            await _unitOfWork.Repository<Order>().AddAsync(order);

            //9.Save Database 
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;

            //10.Send notification for create
            _emailServices.SendEmail(order);

            // 14. Return the created order
            return _mapper.Map<OrderResultDto>(order);


        }
        #endregion

        #region Get All Order
        public async Task<IReadOnlyList<OrderResultDto>> GetOrderForUser()
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            return _mapper.Map<IReadOnlyList<OrderResultDto>>(orders);
        }

        #endregion

        #region Get Order By Id
        public async Task<OrderResultDto> GetOrderForUserById(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetAsync(orderId);
            return _mapper.Map<OrderResultDto>(order);
        }

        #endregion

        #region Update Order
        public async Task<OrderResultDto?> UpdateOrderAsync(int orderId, OrderDto input)
        {

            // 1. Get the order by ID
            var orderGet = await _unitOfWork.Repository<Order>().GetAsync(orderId);

            // 2. Check if order exists
            if (orderGet is null)
                throw new ArgumentException($"Order with ID '{orderId}' not found.");

            //3.check validation

            if (!await _validateOrder.ValidateOrder(input))
                throw new InvalidOperationException("Order validation failed.");


            //4.Get Selected Item

            var OrderItems = new List<OrderItem>();
            foreach (var item in input.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.ProductId);
                var orderItem = new OrderItem(product.Id, product.Price, item.Quantity);
                OrderItems.Add(orderItem);
            }

            // 5.Calculate Total Amount
            var totalAmount = ((double)input.Items.Sum(item => item.UnitPrice * item.Quantity));

            // 6.Apply discount

            var discountedAmount = totalAmount;
            if (totalAmount > 200)
            {
                discountedAmount *= 0.9; // 10% discount
            }
            else if (totalAmount > 100)
            {
                discountedAmount *= 0.95; // 5% discount
            }


            //7.Update product stock

            foreach (var item in input.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.ProductId);
                product.Stock -= item.Quantity;
                _unitOfWork.Repository<Product>().Update(product);

            }

  
            //8. Generate invoice

            var invoice = new InvoiceDto
            {
                OrderId = input.Id,
                InvoiceDate = DateTimeOffset.UtcNow,
                TotalAmount = totalAmount
            };

            _unitOfWork.Repository<Invoice>().Update(_mapper.Map<Invoice>(invoice));
            // 9. Map From OrderItemDto => OrderITem 
            var mappedItems = _mapper.Map<List<OrderItem>>(OrderItems);

            //10.Create Oder 
            var order = new Order()
            {
                CustomerId = input.CustomerId,
                OrderDate = DateTimeOffset.UtcNow,
                orderStatus = OrderStatus.Pending,
                Items = mappedItems,
                TotalAmount = totalAmount,
                PriceWithDiscount = discountedAmount,
            };


            //11.Update Order
            _unitOfWork.Repository<Order>().Update(order);

            //12. Save changes to the database
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0)
                return null;

            //13.Send notification for update
            _emailServices.SendEmail(order);

            // 14. Return the updated order
            return _mapper.Map<OrderResultDto>(order);



        }

        #endregion





    }
}


