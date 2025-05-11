using QuanLySach.Business.Interfaces;
using QuanLySach.Common.Exceptions;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Mappers;
using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDataService _orderDataService;
        private readonly ICustomerDataService _customerDataService;
        private readonly IBookDataService _bookDataService;

        public OrderLogic(IOrderDataService orderDataService, ICustomerDataService customerDataService, IBookDataService bookDataService)
        {
            _orderDataService = orderDataService;
            _customerDataService = customerDataService;
            _bookDataService = bookDataService;
        }

        public async Task<List<Order>> GetOrders(DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.MinValue;
            to = to ?? DateTime.MaxValue;
            var entities = await _orderDataService.Get(from.Value, to.Value);

          return OrderMapper.ToDomain(entities.ToList());
        }

        public async Task<Order> GetOrder(int? orderId)
        {
            if (orderId == null || orderId <= 0)
            {
                throw new InvalidDataException();
            }

            var order = await _orderDataService.GetFullOrder(orderId.Value);

            return OrderMapper.ToDomain(order);
        }

        public async Task SaveOrder(DomainModels.Order order)
        {
            ValidateOrder(order);
            ValidateProductQuantity(order);

            if (order.Id == 0)
            {
                var customer = await _customerDataService.Get(order.Customer.PhoneNumber);
                if (!string.IsNullOrEmpty(order.Customer.PhoneNumber) && customer != null)
                {
                    order.Customer = null;
                    var orderEntity = OrderMapper.ToEntity(order);
                    orderEntity.CustomerId = customer.Id;
                    await _orderDataService.Add(orderEntity);
                }
                else
                {
                    await _orderDataService.Add(OrderMapper.ToEntity(order));
                }
            }
            else
            {
                var orderEntity = OrderMapper.ToEntity(order);
                foreach (var orderEntityOrderDetail in orderEntity.OrderDetails)
                {
                    orderEntityOrderDetail.Id = 0;
                }

                await _orderDataService.RemoveAllOrderItems(orderEntity);
                await _orderDataService.Update(order.Id, orderEntity);
            }
            _bookDataService.UpdateProductQuantity(OrderDetailMapper.ToEntity(order.OrderDetails));
        }

        private async void ValidateProductQuantity(Order order)
        {
            var result = await _bookDataService.CheckProductQuantity(OrderDetailMapper.ToEntity(order.OrderDetails));
            if (!result)
            {
                throw new OrderValidationException("Sản phẩm đã hết hàng");
            }
        }

        public async Task Remove(Order order)
        {
            if (order == null || order.Id <= 0)
            {
                throw new InvalidDataException();
            }

            await _orderDataService.Delete(order.Id);
        }

        private static void ValidateOrder(DomainModels.Order order)
        {
            if (order.OrderDetails.Count == 0)
            {
                throw new OrderValidationException("Xin vui lòng chọn sản phẩm");
            }

            if (order.Customer == null || (string.IsNullOrEmpty(order.Customer.FullName) && string.IsNullOrEmpty(order.Customer.PhoneNumber)))
            {
                throw new OrderValidationException("Xin vui lòng nhập tên hoặc sdt khách hàng");
            }
        }
    }
}
