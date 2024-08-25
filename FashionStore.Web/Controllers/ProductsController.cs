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
        var product = await _context.products.Include(x => x.Colors).Include(b => b.Sizes).Select(p => new ProductViewModel
        {
            Name = p.Name,
            Description = p.Description,
            ShortDescription = p.ShortDescription,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            ImageUrl = p.ImageUrl,
            AvailableColors = p.Colors.Select(c => new ColorViewModel
            {
                Name = c.Name,
                HexValue = c.HexValue,
                ImageUrl = c.ImageUrl,

            }).ToList(),
            AvailableSizes = p.Sizes.Select(a => new SizeViewModel
            {
                ProductSize = a.ProductSize
            }).ToList(),
        }).ToListAsync();
        return View(product);
    }


    [HttpGet]
    public IActionResult CreateProduct()
    {
        var model = new ProductCreateViewModel
        {
            AvailableColors = _context.colors.Select(c => new ColorViewModel
            {
                Id = c.Id,
                Name = c.Name,
                HexValue = c.HexValue
            }).ToList(),

            AvailableSizes = _context.sizes.Select(s => new SizeViewModel
            {
                Id = s.Id,
                ProductSize = s.ProductSize
            }).ToList(),
        };
        ViewBag.SubCategories = new SelectList(_context.subCategories, "Id", "Name");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateViewModel model, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                SubCategoryId = model.SubCategoryId
            };

            // Handle image upload
            if (image != null)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                product.ImageUrl = "Images/" + fileName;
            }

            // Add selected colors and sizes
            product.Colors = _context.colors.Where(c => model.SelectedColors.Contains(c.Id)).ToList();
            product.Sizes = _context.sizes.Where(s => model.SelectedSizes.Contains(s.Id)).ToList();

            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Reload dropdowns and checkbox options in case of validation errors
        model.AvailableColors = _context.colors.Select(c => new ColorViewModel
        {
            Id = c.Id,
            Name = c.Name,
            HexValue = c.HexValue
        }).ToList();

        model.AvailableSizes = _context.sizes.Select(s => new SizeViewModel
        {
            Id = s.Id,
            ProductSize = s.ProductSize
        }).ToList();

        //model.SubCategories = new SelectList(_context.subCategories, "Id", "Name");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _context.products
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            ImageUrl = product.ImageUrl,
            SelectedColors = product.Colors.Select(c => c.Id).ToList(),
            SelectedSizes = product.Sizes.Select(s => s.Id).ToList(),
            SubCategoryId = product.SubCategoryId,

            AvailableColors = _context.colors.Select(c => new ColorViewModel
            {
                Id = c.Id,
                Name = c.Name,
                HexValue = c.HexValue
            }).ToList(),

            AvailableSizes = _context.sizes.Select(s => new SizeViewModel
            {
                Id = s.Id,
                ProductSize = s.ProductSize
            }).ToList()
        };

        ViewBag.SubCategories = new SelectList(_context.subCategories, "Id", "Name", model.SubCategoryId);

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductEditViewModel model, IFormFile image)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.SubCategories = new SelectList(_context.subCategories, "Id", "Name", model.SubCategoryId);
            return View(model);
        }

        var product = await _context.products
            .Include(p => p.Colors)
            .Include(p => p.Sizes)
            .FirstOrDefaultAsync(p => p.Id == model.Id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = model.Name;
        product.ShortDescription = model.ShortDescription;
        product.Description = model.Description;
        product.Price = model.Price;
        product.StockQuantity = model.StockQuantity;
        product.SubCategoryId = model.SubCategoryId;

        // Handle image upload if new image is provided
        // Handle image upload separately
        if (image != null)
        {
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "Images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            product.ImageUrl = "Images/" + fileName; // Save the new image path
        }

        // Update colors
        product.Colors.Clear();
        product.Colors = _context.colors.Where(c => model.SelectedColors.Contains(c.Id)).ToList();

        // Update sizes
        product.Sizes.Clear();
        product.Sizes = _context.sizes.Where(s => model.SelectedSizes.Contains(s.Id)).ToList();

        _context.Update(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productsService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            ImageUrl = product.ImageUrl
        };

        return View(model);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productsService.Delete(id);
        return RedirectToAction("Index");
    }
}
