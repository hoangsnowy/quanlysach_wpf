using QuanLySach.DomainModels;

namespace QuanLySach.DAL.Excel.Interfaces
{
    public interface IExcelProductDataService
    {
        Tuple<List<Category>, List<Book>> ProcessFile(string fileName);
    }
}
