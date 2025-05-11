using QuanLySach.DomainModels;

namespace QuanLySach.DAL.Interfaces
{
    public interface ICredentialDataService
    {
        public User GetUserCredential();
        public void AddUserCredential(string email, string hashedPassword, bool rememberedMe);
    }
}
