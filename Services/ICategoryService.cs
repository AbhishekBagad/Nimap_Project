using Nimap_Project.Models;

namespace Nimap_Project.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        public Category GetCategoryById(int id);

        public int AddCategory(Category category);

        public int UpdateCategory(Category category);

        public int DeleteCategory(int id);

    }
}
