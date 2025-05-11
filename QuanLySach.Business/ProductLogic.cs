using QuanLySach.Business.Interfaces;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Mappers;
using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public class ProductLogic : IProductLogic
    {
        private readonly IBookDataService _bookDataService;

        public ProductLogic(IBookDataService bookDataService)
        {
            _bookDataService = bookDataService;
        }

        public async Task<List<Book>> GetProductsByCategoryId(int? categoryId)
        {
            IEnumerable<DAL.EF.Models.Book> entities;
            if (categoryId == null || categoryId == 0)
            {
                entities = await _bookDataService.GetAll();
            }
            else
            {
                entities = await _bookDataService.GetByCategoryId(categoryId.Value);
            }

            return BookMapper.ToDomain(entities.ToList());
        }

        public async Task<List<Book>> Search(int? categoryId, string keyword)
        {

            IEnumerable<DAL.EF.Models.Book> entities;
            if (categoryId == null || categoryId == 0)
            {
                entities = await _bookDataService.GetByKeyword(keyword);
            }
            else
            {
                entities = await _bookDataService.GetByCategoryAndKeyword(categoryId.Value, keyword);
            }

            return BookMapper.ToDomain(entities.ToList());
        }

        public async Task<bool> Delete(Book book)
        {
            if (book == null || book.Id <= 0)
            {
                throw new InvalidDataException();
            }

            return await _bookDataService.Delete(book.Id);
        }

        public async Task AddNewProduct(Book book)
        {
            if (book == null || book.Id > 0)
            {
                throw new InvalidDataException();
            }

            await _bookDataService.Add(BookMapper.ToEntity(book));
        }

        public async Task UpdateProduct(Book book)
        {
            if (book == null || book.Id < 0)
            {
                throw new InvalidDataException();
            }

            await _bookDataService.Update(book.Id, BookMapper.ToEntity(book));
        }
    }
}
