using FashionStore.Business.CategoryService;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.Web.Controllers;

public class CategoryController : Controller
{
    private ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategories();
        return View(categories);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        await _categoryService.Create(category);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _categoryService.GetById(id);
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
        await _categoryService.Update(category);
        return RedirectToAction("Index");
    }

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
