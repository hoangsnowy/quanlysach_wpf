using Microsoft.EntityFrameworkCore;

namespace QuanLySach.DAL.EF
{
    public class BookDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public BookDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public BookDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<BookDbContext> options = new DbContextOptionsBuilder<BookDbContext>();

            _configureDbContext(options);

            return new BookDbContext(options.Options);
        }
    }
}
