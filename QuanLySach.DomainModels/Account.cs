namespace QuanLySach.DomainModels
{
    public class Account
    {
        public User? CurrentUser { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
