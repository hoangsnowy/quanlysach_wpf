using Microsoft.EntityFrameworkCore;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF
{
    public class CustomerDataService : GenericDataService<Customer>, ICustomerDataService
    {
        public CustomerDataService(BookDbContextFactory contextFactory, NonQueryDataService<Customer> nonQueryDataService)
            : base(contextFactory, nonQueryDataService)
        {
        }

        public async Task<Customer?> Get(string phoneNumber)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Customers.FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);
            }
        }
    }
}
