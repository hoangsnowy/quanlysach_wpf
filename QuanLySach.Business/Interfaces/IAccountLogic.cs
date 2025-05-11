using QuanLySach.DomainModels;

namespace QuanLySach.Business.Interfaces
{
    public interface IAccountLogic
    {
        Account GetCurrentAccount();
        void StoreAccount(string email, string password, bool rememberedMe);
    }
}
