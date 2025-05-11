using QuanLySach.DAL.EF.Models;
using QuanLySach.DAL.Interfaces;

namespace QuanLySach.DAL.EF.Interfaces
{
    public interface ICustomerDataService : IDataService<Customer>
    {
        Task<Customer?> Get(string phoneNumber);
    }
}
