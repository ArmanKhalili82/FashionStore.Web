using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.SubCategoryService;

public class SubCategoryService: ISubCategoryService
{
    private readonly ApplicationDbContext _context;

    public SubCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateSubCategory(SubCategory subCategory)
    {
        await _context.subCategories.AddAsync(subCategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubCategory(int id)
    {
        var subcategory = await _context.subCategories.FindAsync(id);
        _context.subCategories.Remove(subcategory);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubCategory>> GetSubCategories()
    {
        var subcategories = await _context.subCategories.Include(x => x.Category).ToListAsync();
        return subcategories;
    }

    public async Task<SubCategory> GetSubCategoryById(int id)
    {
        var subcategory = await _context.subCategories.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        return subcategory;
    }

    public async Task UpdateSubCategory(SubCategory subCategory)
    {
        _context.subCategories.Update(subCategory);
        await _context.SaveChangesAsync();
    }
}
