using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.DAL.EF.Models
{
    [Table("Book")]
    public class Book : EntityBase
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? CoverImagePath { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Author { get; set; }
        public DateTime? PublishedDate { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails  { get; set; }
}
}
