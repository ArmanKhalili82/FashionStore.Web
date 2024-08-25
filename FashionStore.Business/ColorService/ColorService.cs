using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.ColorService;

public class ColorService : IColorService
{
    private readonly ApplicationDbContext _context;

    public ColorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Color color)
    {
        await _context.colors.AddAsync(color);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int colorId)
    {
        var color = await _context.colors.FindAsync(colorId);
        _context.colors.Remove(color);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Color>> GetAllColors()
    {
        var colors = await _context.colors.ToListAsync();
        return colors;
    }

    public async Task<Color> GetById(int id)
    {
        var color = await _context.colors.FindAsync(id);
        return color;
    }

    public async Task Update(Color color)
    {
        _context.colors.Update(color);
        await _context.SaveChangesAsync();
    }
}
