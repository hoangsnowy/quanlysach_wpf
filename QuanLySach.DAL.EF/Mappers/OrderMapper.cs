using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF.Mappers
{
    public static class OrderMapper
    {
        public static List<Order> ToEntity(List<DomainModels.Order> orders)
        {
            return orders.Select(ToEntity).ToList();
        }

        public static Order ToEntity(DomainModels.Order order)
        {
            return new Order
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                OrderTime = order.OrderTime,
                Customer = CustomerMapper.ToEntity(order.Customer),
                OrderDetails = OrderDetailMapper.ToEntity(order.OrderDetails)
            };
        }

        public static List<DomainModels.Order> ToDomain(List<Order> orders)
        {
            return orders.Select(ToDomain).ToList();
        }

        public static DomainModels.Order ToDomain(Order order)
        {
            return new DomainModels.Order
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                OrderTime = order.OrderTime,
                Customer = CustomerMapper.ToDomain(order.Customer),
                OrderDetails = OrderDetailMapper.ToDomain(order.OrderDetails)
            };
        }
    }
}
