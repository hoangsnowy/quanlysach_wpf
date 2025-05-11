using Microsoft.EntityFrameworkCore;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF
{
    public class OrderDataService : GenericDataService<Order>, IOrderDataService
    {
        public OrderDataService(BookDbContextFactory contextFactory, NonQueryDataService<Order> nonQueryDataService) 
            : base(contextFactory, nonQueryDataService)
        {
        }

        public async Task<IEnumerable<Order>> Get(DateTime from, DateTime to)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Order> entities = await context.Orders
                    .Include(q => q.OrderDetails)
                    .ThenInclude(q => q.Book)
                    .ThenInclude(q => q.Category)
                    .Include(q => q.Customer)
                    .Where(b => b.OrderTime.Date >= from.Date && b.OrderTime.Date <= to.Date)
                    .OrderBy(q => q.OrderTime)
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<Order?> GetFullOrder(int orderId)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                Order entity = await context.Orders
                    .Include(q => q.OrderDetails)
                    .ThenInclude(q => q.Book)
                    .ThenInclude(q => q.Category)
                    .Include(q => q.Customer)
                    .SingleAsync(b => b.Id == orderId);
                return entity;
            }
        }

        public async Task RemoveAllOrderItems(Order order)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                var orderDetails = await context.OrderDetails.Where(q => q.OrderId == order.Id).ToListAsync();
                context.OrderDetails.RemoveRange(orderDetails);
                await context.SaveChangesAsync();
            }
        }

        public int GetOrderCount(DateTime from, DateTime to)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Orders
                    .Count(b => b.OrderTime.Date >= @from.Date && b.OrderTime.Date <= to.Date);
            }
        }
    }
}
