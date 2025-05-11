using QuanLySach.Business.Interfaces;
using QuanLySach.Common.Helpers;
using QuanLySach.DAL.Interfaces;
using QuanLySach.DomainModels;
using User = QuanLySach.DAL.EF.Models.User;

namespace QuanLySach.Business
{
    public class AuthenticationLogic : IAuthenticationLogic
    {
        private readonly IUserDataService _userDataService;

        public AuthenticationLogic(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            string hashedPassword = SecureHelper.ComputeSha256Hash(password);

            User? entity = await _userDataService.GeUser(email, hashedPassword);

            return entity != null;
        }

        public async Task<bool> SignInWithHashedPassword(string email, string hashedPassword)
        {
            User? entity = await _userDataService.GeUser(email, hashedPassword);

            return entity != null;
        }
    }
}
