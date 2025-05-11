using QuanLySach.DomainModels;
using OrderDetail = QuanLySach.DAL.EF.Models.OrderDetail;

namespace QuanLySach.DAL.EF.Mappers
{
    public static class OrderDetailMapper
    {
        public static List<OrderDetail> ToEntity(List<DomainModels.OrderDetail> orderDetails)
        {
            return orderDetails.Select(ToEntity).ToList();
        }

        public static OrderDetail ToEntity(DomainModels.OrderDetail orderDetail)
        {
            return new OrderDetail
            {
                Id = orderDetail.Id,
                BookId = orderDetail.Book.Id,
                Quantity = orderDetail.Quantity,
                TotalPrice = orderDetail.TotalPrice,
                UnitPrice = orderDetail.UnitPrice
            };
        }

        public static List<DomainModels.OrderDetail> ToDomain(ICollection<OrderDetail> orderDetails)
        {
            return orderDetails.Select(ToDomain).ToList();
        }

        public static DomainModels.OrderDetail ToDomain(OrderDetail orderDetail)
        {
            return new DomainModels.OrderDetail
            {
                Id = orderDetail.Id,
                Quantity = orderDetail.Quantity,
                TotalPrice = orderDetail.TotalPrice,
                UnitPrice = orderDetail.UnitPrice,
                Book = BookMapper.ToDomain(orderDetail.Book)
            };
        }
    }
}
