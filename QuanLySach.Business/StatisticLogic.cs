using QuanLySach.Business.Interfaces;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Mappers;
using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public class StatisticLogic : IStatisticLogic
    {
        private readonly IOrderDataService _orderDataService;
        private readonly ICustomerDataService _customerDataService;
        private readonly IBookDataService _bookDataService;

        public StatisticLogic(IOrderDataService orderDataService, ICustomerDataService customerDataService, IBookDataService bookDataService)
        {
            _orderDataService = orderDataService;
            _customerDataService = customerDataService;
            _bookDataService = bookDataService;
        }

        public List<Book> GetTopFiveProducts()
        {
            var entities =  _bookDataService.GetTopFiveSaleProduct();

            return BookMapper.ToDomain(entities.ToList()).ToList();
        }

        public async Task<List<KeyValue>> GetRevenueValues(ReportTerm reportTerm)
        {
            var list = new List<KeyValue>();
            DateTime dt = DateTime.Now;

            if (reportTerm == ReportTerm.Day)
            {
                var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var orders = await _orderDataService.Get(firstDayOfMonth, lastDayOfMonth);
                var groupData = orders.GroupBy(q => q.OrderTime.Day).Select(q => new KeyValue()
                {
                    Key = q.Key.ToString(),
                    Value = q.Sum(x => (double)x.TotalPrice)
                });

                return groupData.ToList();
            }

            if (reportTerm == ReportTerm.Week)
            {
                var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var orders = await _orderDataService.Get(firstDayOfMonth, lastDayOfMonth);
                var groupData = orders.GroupBy(q => q.OrderTime.DayOfWeek).Select(q => new KeyValue()
                {
                    Key = q.Key.ToString(),
                    Value = q.Sum(x => (double)x.TotalPrice)
                });

                return groupData.ToList();
            }

            if (reportTerm == ReportTerm.Month)
            {
                var firstDayOfYear = new DateTime(dt.Year, 1, 1);
                var lastDayOfYear = new DateTime(dt.Year, 12, 31);
                var orders = await _orderDataService.Get(firstDayOfYear, lastDayOfYear);

                var groupData = orders.GroupBy(q => q.OrderTime.Month).Select(q => new KeyValue()
                {
                    Key = q.Key.ToString(),
                    Value = q.Sum(x => (double)x.TotalPrice)
                });

                return groupData.ToList();
            }

            if (reportTerm == ReportTerm.Year)
            {
                var firstDayOfYear = new DateTime(dt.Year, 1, 1);
                var lastDayOfYear = new DateTime(dt.Year, 12, 31);
                var orders = await _orderDataService.Get(firstDayOfYear, lastDayOfYear);

                var groupData = orders.GroupBy(q => q.OrderTime.Year).Select(q => new KeyValue()
                {
                    Key = q.Key.ToString(),
                    Value = q.Sum(x => (double)x.TotalPrice)
                });

                return groupData.ToList();
            }

            return new List<KeyValue>();
        }


        public DashboardStatistic GetDashboardStatistic()
        {
            int totalOrder = _orderDataService.Count();
            int totalCustomer = _customerDataService.Count();

            DateTime dt = DateTime.Now;
            DateTime firstDay = dt.AddDays(-(int)dt.DayOfWeek);
            DateTime endDay = firstDay.AddDays(6);

            int totalOrderInWeek = _orderDataService.GetOrderCount(firstDay, endDay);

            return new DashboardStatistic
            {
                TotalCustomers = totalCustomer,
                TotalOrders = totalOrder,
                TotalOrdersInWeek = totalOrderInWeek
            };
        }
    }
}
