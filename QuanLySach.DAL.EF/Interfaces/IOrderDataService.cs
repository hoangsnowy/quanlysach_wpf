using QuanLySach.DAL.Interfaces;
using Order = QuanLySach.DAL.EF.Models.Order;

namespace QuanLySach.DAL.EF.Interfaces
{
    public interface IOrderDataService : IDataService<Order>
    {
        public Task<IEnumerable<Order>> Get(DateTime from, DateTime to);
        public Task<Order> GetFullOrder(int orderId);
        public Task RemoveAllOrderItems(Order order);
        public int GetOrderCount(DateTime from, DateTime to);
    }
}
