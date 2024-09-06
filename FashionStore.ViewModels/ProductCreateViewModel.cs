using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class ProductCreateViewModel
{
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    //public IFormFile Image { get; set; } // For uploading the image
    public bool IsFeatured { get; set; }
    public string Image {  get; set; }
    // For selecting available colors
    public List<int> SelectedColors { get; set; }
    public List<ColorViewModel> AvailableColors { get; set; }

    // For selecting available sizes
    public List<int> SelectedSizes { get; set; }
    public List<SizeViewModel> AvailableSizes { get; set; }

    // For selecting a subcategory
    public int SubCategoryId { get; set; }
    //public SelectList SubCategories { get; set; }
}

