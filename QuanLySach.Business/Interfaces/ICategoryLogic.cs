using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public interface ICategoryLogic
    {
        Task<List<Category>> GetAllCategories();
    }
}
