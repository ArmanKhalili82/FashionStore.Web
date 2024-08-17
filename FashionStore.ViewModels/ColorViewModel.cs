using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class ColorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HexValue { get; set; }
    public string ImageUrl { get; set; }
}
