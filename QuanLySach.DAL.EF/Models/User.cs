using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.DAL.EF.Models
{
    [Table("User")]
    public class User : EntityBase
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? LastName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string PasswordHashed { get; set; }
    }
}
