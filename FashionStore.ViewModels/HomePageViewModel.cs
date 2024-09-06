using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class HomePageViewModel
{
    public List<Product> ShowAllProducts { get; set; }
    public List<Product> CarouselProducts { get; set; }
    public List<Product> FeaturedProducts { get; set; }
    public List<Product> PopularProducts { get; set; }
}
