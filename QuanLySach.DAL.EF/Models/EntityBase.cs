using System.ComponentModel.DataAnnotations;

namespace QuanLySach.DAL.EF.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
