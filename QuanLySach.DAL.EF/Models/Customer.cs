using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.DAL.EF.Models
{
    [Table("Customer")]
    public class Customer : EntityBase
    {
        [Column(TypeName = "nvarchar(255)")]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? IdentityCardNumber { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Address { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
