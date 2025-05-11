using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.Interfaces
{
    public interface IDataService<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        void AddRange(IList<T> entities);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        int Count();
    }
}
