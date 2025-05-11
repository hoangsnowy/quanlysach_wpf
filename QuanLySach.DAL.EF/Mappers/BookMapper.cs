using Book = QuanLySach.DAL.EF.Models.Book;

namespace QuanLySach.DAL.EF.Mappers
{
    public static class BookMapper
    {
        public static List<Book> ToEntity(List<DomainModels.Book> books)
        {
            return books.Select(ToEntity).ToList();
        }

        public static Book ToEntity(DomainModels.Book book)
        {
            return new Book
            {
                Name = book.Name,
                Author = book.Author,
                CoverImagePath = book.CoverImagePath,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Quantity = book.Quantity,
                CategoryId = book.Category.Id
            };
        }

        public static List<DomainModels.Book> ToDomain(List<Book> books)
        {
            return books.Select(ToDomain).ToList();
        }

        public static DomainModels.Book ToDomain(Book book)
        {
            return new DomainModels.Book
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                CoverImagePath = book.CoverImagePath,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Quantity = book.Quantity,
                Category = new DomainModels.Category()
                {
                    Id = book.Category.Id,
                    Name = book.Category.Name
                }
            };
        }
    }
}
