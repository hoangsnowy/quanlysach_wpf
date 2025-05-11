using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.DAL.EF.Models
{
    [Table("OrderDetail")]
    public class OrderDetail : EntityBase
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
