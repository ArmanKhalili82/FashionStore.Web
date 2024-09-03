using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Web.Controllers;

public class SizeController : Controller
{
    private readonly ApplicationDbContext _context;

    public SizeController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var sizes = await _context.sizes.ToListAsync();
        return View(sizes);
    }

    [HttpGet]
    public IActionResult CreateSize()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSize(Size size)
    {
        await _context.sizes.AddAsync(size);
        await _context.SaveChangesAsync();
        return RedirectToAction("index");
    }

    [HttpGet]
    public async Task<IActionResult> EditSize(int id)
    {
        var size = await _context.sizes.FindAsync(id);
        return View(size);
    }

    [HttpPost]
    public async Task<IActionResult> EditSize(Size size)
    {
        _context.sizes.Update(size);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteSize(int id)
    {
        var size = await _context.sizes.FindAsync(id);
        return View(size);
    }

    [HttpPost, ActionName("DeleteSize")]
    public async Task<IActionResult> Delete(int id)
    {
        var size = await _context.sizes.FindAsync(id);
        _context.sizes.Remove(size);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
