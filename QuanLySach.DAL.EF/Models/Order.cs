using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.DAL.EF.Models
{
    [Table("Order")]
    public class Order : EntityBase
    {
        public DateTime OrderTime { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
