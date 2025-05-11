using QuanLySach.DAL.EF.Models;
using QuanLySach.DAL.Interfaces;

namespace QuanLySach.DAL.EF.Interfaces
{
    public interface IBookDataService : IDataService<Book>
    {
        public IEnumerable<Book> GetTopFiveSaleProduct();
        public Task<IEnumerable<Book>> GetByCategoryId(int categoryId);
        public Task<IEnumerable<Book>> GetByCategoryAndKeyword(int categoryId, string keyword);
        public Task<IEnumerable<Book>> GetByKeyword(string keyword);
        public Task<bool> CheckProductQuantity(List<OrderDetail> orderDetails);
        public void UpdateProductQuantity(List<OrderDetail> orderDetails);
    }
}
