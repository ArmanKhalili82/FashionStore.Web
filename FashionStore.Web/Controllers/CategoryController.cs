using FashionStore.Business.CategoryService;
using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Web.Controllers;

public class CategoryController : Controller
{
    private ICategoryService _categoryService;
    private readonly ApplicationDbContext _context;
    public CategoryController(ICategoryService categoryService, ApplicationDbContext context)
    {
        _categoryService = categoryService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategories();
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        // Load parent categories for the dropdown list
        ViewBag.ParentCategories = new SelectList(_context.categories, "Id", "Name");

        // If id is null, it's a new category (create mode)
        if (id == null)
        {
            return View(new Category());
        }

        // Otherwise, it's an existing category (edit mode)
        var category = await _context.categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Category category)
    {
        if (ModelState.IsValid)
        {
            if (category.Id == 0)  // Create new category
            {
                await _categoryService.Create(category);
            }
            else  // Update existing category
            {
                await _categoryService.Update(category);
            }
            return RedirectToAction(nameof(Index));
        }
        // Repopulate ParentCategories for the dropdown in case of validation errors
        ViewBag.ParentCategories = new SelectList(_context.categories, "Id", "Name", category.CategoryId);
        return View(category);
    }

    //[HttpGet]
    //public IActionResult CreateCategory()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> CreateCategory(Category category)
    //{
    //    await _categoryService.Create(category);
    //    return RedirectToAction("Index");
    //}

    //[HttpGet]
    //public async Task<IActionResult> EditCategory(int id)
    //{
    //    var category = await _categoryService.GetById(id);
    //    return View(category);
    //}

    //[HttpPost]
    //public async Task<IActionResult> EditCategory(Category category)
    //{
    //    await _categoryService.Update(category);
    //    return RedirectToAction("Index");
    //}

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _categoryService.GetById(id);
        return View(category);
    }

    [HttpPost, ActionName("DeleteCategory")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.Delete(id);
        return RedirectToAction("Index");
    }
}
