using Microsoft.EntityFrameworkCore;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF
{
    public class BookDataService : GenericDataService<Book>, IBookDataService
    {
        public BookDataService(BookDbContextFactory contextFactory, NonQueryDataService<Book> nonQueryDataService)
            : base(contextFactory, nonQueryDataService)
        {
        }

        public IEnumerable<Book> GetTopFiveSaleProduct()
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Book> entities = context.OrderDetails
                    .Include(q => q.Book)
                    .ThenInclude(q => q.Category)
                    .ToList()
                    .GroupBy(q => q.Book)
                    .Select(q => new
                    {
                        TotalQuantity = q.Sum(x => x.Quantity),
                        Book = q.Key
                    })
                    .OrderByDescending(q => q.TotalQuantity)
                    .Take(5)
                    .Select(q => q.Book);

                return entities;
            }
        }

        public async Task<IEnumerable<Book>> GetByCategoryId(int categoryId)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Book> entities = await context.Books
                    .Include(q => q.Category)
                    .Where(b => b.CategoryId == categoryId)
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<Book>> GetByCategoryAndKeyword(int categoryId, string keyword)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Book> entities = await context.Books
                    .Include(q => q.Category)
                    .Where(b => b.CategoryId == categoryId && (b.Name.Contains(keyword) || b.Author.Contains(keyword) || b.Id.ToString().Contains(keyword)))
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<Book>> GetByKeyword(string keyword)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Book> entities = await context.Books
                    .Include(q => q.Category)
                    .Where(b => b.Name.Contains(keyword) || b.Author.Contains(keyword) || b.Id.ToString().Contains(keyword))
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<bool> CheckProductQuantity(List<OrderDetail> orderDetails)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<int> bookListId = orderDetails.Select(q => q.BookId);
                var books = await context.Books.Where(q => bookListId.Contains(q.Id)).ToListAsync();

                foreach (var book in books)
                {
                    var orderDetail = orderDetails.Single(q => q.BookId == book.Id);
                    if (orderDetail.Quantity > book.Quantity)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void UpdateProductQuantity(List<OrderDetail> orderDetails)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<int> bookListId = orderDetails.Select(q => q.BookId);
                var books = context.Books.Where(q => bookListId.Contains(q.Id)).ToList();

                foreach (var book in books)
                {
                    var orderDetail = orderDetails.Single(q => q.BookId == book.Id);
                    book.Quantity -= orderDetail.Quantity;
                }

                context.SaveChanges();
            }
        }
    }
}
