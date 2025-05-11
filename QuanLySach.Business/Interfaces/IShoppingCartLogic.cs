using QuanLySach.DomainModels;

namespace QuanLySach.Business.Interfaces
{
    public interface IOrderLogic
    {
        Task<List<Order>> GetOrders(DateTime? from, DateTime? to);
        Task<Order> GetOrder(int? orderId);
        Task SaveOrder(DomainModels.Order order);
        Task Remove(Order order);
    }
}
