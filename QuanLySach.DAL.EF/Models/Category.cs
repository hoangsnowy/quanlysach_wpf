using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.DAL.EF.Models
{
    [Table("Category")]
    public class Category : EntityBase
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
