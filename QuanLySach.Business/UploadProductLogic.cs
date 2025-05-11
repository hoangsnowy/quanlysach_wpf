using QuanLySach.Business.Interfaces;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Mappers;
using QuanLySach.DAL.Excel.Interfaces;
using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public class UploadProductLogic : IUploadProductLogic
    {
        private readonly IExcelProductDataService _excelProductDataService;
        private readonly IBookDataService _bookDataService;
        private readonly ICategoryDataService _categoryDataService;

        public UploadProductLogic(IExcelProductDataService excelProductDataService, IBookDataService bookDataService, ICategoryDataService categoryDataService)
        {
            _excelProductDataService = excelProductDataService;
            _bookDataService = bookDataService;
            _categoryDataService = categoryDataService;
        }

        public void Upload(string fileName)
        {
            Tuple<List<Category>, List<Book>> result = _excelProductDataService.ProcessFile(fileName);

            if (_categoryDataService.Count() == 0)
            {
                var categories = CategoryMapper.ToEntity(result.Item1);
                _categoryDataService.AddRange(categories);
            }

            var books = BookMapper.ToEntity(result.Item2);
            _bookDataService.AddRange(books);
        }
    }
}
