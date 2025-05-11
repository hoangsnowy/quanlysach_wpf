using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.Interfaces
{
    public interface IUserDataService : IDataService<User>
    {
        Task<User?> GeUser(string email, string hashPassword);
    }
}
