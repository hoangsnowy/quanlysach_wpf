using QuanLySach.Business.Interfaces;
using QuanLySach.Common.Helpers;
using QuanLySach.DAL.Interfaces;
using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public class AccountLogic : IAccountLogic
    {
        private readonly ICredentialDataService _credentialDataService;
        public AccountLogic(ICredentialDataService credentialDataService)
        {
            _credentialDataService = credentialDataService;
        }

        public Account GetCurrentAccount()
        {
            User? user = _credentialDataService.GetUserCredential();

            if (user == null)
                return null;

            return new Account()
            {
                CurrentUser = user
            };
        }

        public void StoreAccount(string email, string password, bool rememberedMe)
        {
            _credentialDataService.AddUserCredential(email, password, rememberedMe);
        }
    }
}
