using QuanLySach.DomainModels;

namespace QuanLySach.Business.Interfaces
{
    public interface IProductLogic
    {
        Task<List<Book>> GetProductsByCategoryId(int? categoryId);
        Task<List<Book>> Search(int? categoryId, string keyword);
        Task<bool> Delete(Book book);
        Task AddNewProduct(Book bookInfo);
        Task UpdateProduct(Book bookInfo);
    }
}
