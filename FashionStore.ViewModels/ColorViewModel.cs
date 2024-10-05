using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class ColorViewModel
{
    public int Id { get; set; }
    //[Required]
    public string Name { get; set; }
    //[Required]
    //[RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Invalid hex value")]
    public string HexValue { get; set; }
    //public string ImageUrl { get; set; } = "";
    public string? ImageUrl { get; set; }
}
