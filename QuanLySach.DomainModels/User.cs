namespace QuanLySach.DomainModels
{
    public class User
    {
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public bool RememberedMe { get; set; }
    }
}
