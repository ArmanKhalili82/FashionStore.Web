using FashionStore.Business.ProductsService;
using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using FashionStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private IProductsService _productsService;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

    public ProductsController(IProductsService productsService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, ApplicationDbContext context)
    {
        _productsService = productsService;
        _environment = environment;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productsService.GetAllProducts();
        var colors = await _context.colors.ToListAsync();
        var sizes = await _context.sizes.ToListAsync();
        var productVm = new ProductDetailsViewModel
        {
            Products = products,
            AvailableColors = colors,
            AvailableSizes = sizes
        };
        return View(productVm);
    }


    [HttpGet]
    public IActionResult CreateProduct()
    {
        ViewData["SubCategoryId"] = new SelectList(_context.subCategories.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product products, IFormFile image)
    {
        if (image != null)
        {
            var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
            await image.CopyToAsync(new FileStream(name, FileMode.Create));
            products.ImageUrl = "Images/" + image.FileName;
        }
        await _productsService.Create(products);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        ViewData["CategoryId"] = new SelectList(_context.categories.ToList(), "Id", "Name");
        var product = await _productsService.GetProductById(id);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(Product product, IFormFile image)
    {
        if (image != null)
        {

            var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
            await image.CopyToAsync(new FileStream(name, FileMode.Create));
            product.ImageUrl = "Images/" + image.FileName;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                //Delete The Old Image
                var oldImagePath = Path.Combine(name, product.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                product.ImageUrl = "Images/" + image.FileName;
            }
        }
        await _productsService.Update(product);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productsService.GetProductById(id);
        return View(product);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productsService.Delete(id);
        return RedirectToAction("Index");
    }
}
