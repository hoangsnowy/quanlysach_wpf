using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF.Mappers
{
    public static class CategoryMapper
    {
        public static List<Category> ToEntity(List<DomainModels.Category> categories)
        {
            return categories.Select(ToEntity).ToList();
        }

        public static Category ToEntity(DomainModels.Category category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static List<DomainModels.Category> ToDomain(List<Category> categories)
        {
            return categories.Select(ToDomain).ToList();
        }

        public static DomainModels.Category ToDomain(Category category)
        {
            return new DomainModels.Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
