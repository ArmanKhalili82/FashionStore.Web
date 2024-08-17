using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class ProductDetailsViewModel
{
    public List<Product> Products { get; set; }
    public List<Color> AvailableColors { get; set; }
    public List<Size> AvailableSizes { get; set; }
}
