using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF
{
    public class CategoryDataService : GenericDataService<Category>, ICategoryDataService
    {
        public CategoryDataService(BookDbContextFactory contextFactory, NonQueryDataService<Category> nonQueryDataService) 
            : base(contextFactory, nonQueryDataService)
        {
        }
    }
}
