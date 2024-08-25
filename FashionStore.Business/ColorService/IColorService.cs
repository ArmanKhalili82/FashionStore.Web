using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.ColorService;

public interface IColorService
{
    Task<List<Color>> GetAllColors();
    Task<Color> GetById(int id);
    Task Create(Color color);
    Task Update(Color color);
    Task Delete(int colorId);
}
