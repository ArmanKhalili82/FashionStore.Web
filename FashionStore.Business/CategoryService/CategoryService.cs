using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Business.CategoryService;

public class CategoryService: ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Category category)
    {
        await _context.categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int categoryId)
    {
        var category = await _context.categories.FindAsync(categoryId);
        _context.categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var category = await _context.categories.ToListAsync();
        return category;
    }

    public async Task<Category> GetById(int id)
    {
        var category = await _context.categories.FindAsync(id);
        return category;
    }

    public async Task Update(Category category)
    {
        _context.categories.Update(category);
        await _context.SaveChangesAsync();
    }
}
