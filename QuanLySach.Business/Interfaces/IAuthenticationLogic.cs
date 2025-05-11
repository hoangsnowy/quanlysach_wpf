namespace QuanLySach.Business.Interfaces
{
    public interface IAuthenticationLogic
    {
        Task<bool> SignIn(string email, string password);
        Task<bool> SignInWithHashedPassword(string email, string hashedPassword);
    }
}
