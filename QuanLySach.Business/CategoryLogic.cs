using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.EF.Mappers;
using QuanLySach.DomainModels;

namespace QuanLySach.Business
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryDataService _categoryDataService;
        public CategoryLogic(ICategoryDataService categoryDataService)
        {
            _categoryDataService = categoryDataService;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var entities = await _categoryDataService.GetAll();

            return CategoryMapper.ToDomain(entities.ToList());
        }
    }
}
