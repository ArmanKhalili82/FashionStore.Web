using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.ProductsService;

public class ProductsService: IProductsService
{
    private readonly ApplicationDbContext _context;

    public ProductsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Product products)
    {
        await _context.products.AddAsync(products);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int productId)
    {
        var product = await _context.products.FindAsync(productId);
        _context.products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await _context.products.Include(x => x.SubCategory).ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetCarouselProducts()
    {
        var carouselProducts = await _context.products
            .Take(8)
            .ToListAsync();
        return carouselProducts;
    }

    public async Task<List<Product>> GetFeaturedProducts()
    {
        var featuredProducts = await _context.products
            .Where(p => p.IsFeatured)
            .Take(6)
            .ToListAsync();
        return featuredProducts;
    }

    public async Task<List<Product>> GetPopularProducts()
    {
        var poularProducts = await _context.products
            .OrderByDescending(p => p.SalesCount)
            .Take(6)
            .ToListAsync();
        return poularProducts;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _context.products.Include(x => x.SubCategory).FirstOrDefaultAsync(x => x.Id == id);
        return product;
    }

    public async Task Update(Product products)
    {
        _context.products.Update(products);
        await _context.SaveChangesAsync();
    }
}
