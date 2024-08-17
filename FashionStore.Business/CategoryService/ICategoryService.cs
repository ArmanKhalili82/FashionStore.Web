using FashionStore.Models.Models;

namespace FashionStore.Business.CategoryService;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
    Task<Category> GetById(int id);
    Task Create(Category category);
    Task Update(Category category);
    Task Delete(int categoryId);
}
