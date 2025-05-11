using Microsoft.EntityFrameworkCore;
using QuanLySach.DAL.EF.Models;
using QuanLySach.DAL.Interfaces;

namespace QuanLySach.DAL.EF
{
    public class UserDataService : GenericDataService<User>, IUserDataService
    {
        private readonly BookDbContextFactory _contextFactory;
        private readonly NonQueryDataService<User> _nonQueryDataService;

        public UserDataService(BookDbContextFactory contextFactory, NonQueryDataService<User> nonQueryDataService) : base(contextFactory, nonQueryDataService)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = nonQueryDataService;
        }

        public async Task<User?> GeUser(string email, string hashPassword)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(a => a.Email == email && a.PasswordHashed == hashPassword);
            }
        }
    }
}
