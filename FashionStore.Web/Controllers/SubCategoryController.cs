using FashionStore.Business.SubCategoryService;
using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FashionStore.Web.Controllers;

public class SubCategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    private ISubCategoryService _subCategoryService;

    public SubCategoryController(ApplicationDbContext context, ISubCategoryService subCategoryService)
    {
        _context = context;
        _subCategoryService = subCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var subCategories = await _subCategoryService.GetSubCategories();
        return View(subCategories);
    }

    [HttpGet]
    public IActionResult CreateSubCategory()
    {
        ViewData["CategoryId"] = new SelectList(_context.categories.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubCategory(SubCategory subCategory)
    {
        await _subCategoryService.CreateSubCategory(subCategory);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditSubCategory(int id)
    {
        var subCategory = await _subCategoryService.GetSubCategoryById(id);
        return View(subCategory);
    }

    [HttpPost]
    public async Task<IActionResult> EditSubCategory(SubCategory subCategory)
    {
        await _subCategoryService.UpdateSubCategory(subCategory);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        var subCategory = await _subCategoryService.GetSubCategoryById(id);
        return View(subCategory);
    }

    [HttpPost, ActionName("DeleteSubCategory")]
    public async Task<IActionResult> Delete(int id)
    {
        await _subCategoryService.DeleteSubCategory(id);
        return RedirectToAction("Index");
    }
}
