using QuanLySach.DomainModels;

namespace QuanLySach.Business.Interfaces
{
    public interface IStatisticLogic
    {
        DashboardStatistic GetDashboardStatistic();
        Task<List<KeyValue>> GetRevenueValues(ReportTerm reportTerm);
        List<Book> GetTopFiveProducts();
    }
}
