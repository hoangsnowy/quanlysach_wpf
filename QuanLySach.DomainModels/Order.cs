namespace QuanLySach.DomainModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Customer? Customer { get; set; }
    }
}
