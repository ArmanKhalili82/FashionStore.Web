using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class ProductEditViewModel
{
    public int Id { get; set; }  // Product ID

    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsFeatured { get; set; }
    public string? ImageUrl { get; set; } // Existing image URL
    //public IFormFile Image { get; set; } // New image upload

    public List<int> SelectedColors { get; set; }
    public List<ColorViewModel> AvailableColors { get; set; }

    public List<int> SelectedSizes { get; set; }
    public List<SizeViewModel> AvailableSizes { get; set; }

}
