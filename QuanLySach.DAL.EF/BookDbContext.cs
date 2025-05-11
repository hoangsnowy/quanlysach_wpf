using Microsoft.EntityFrameworkCore;
using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Book)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(o => o.BookId);

            modelBuilder.Entity<Book>()
                .HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
