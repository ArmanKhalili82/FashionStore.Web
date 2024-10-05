using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionStore.Models.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Color Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hex Value is required")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$", ErrorMessage = "Hex Value must be in the format #RRGGBB or #RRGGBBAA")]
        public string HexValue { get; set; }
        public string? ImageUrl { get; set; }
        //[DisplayName("Product Name")]
        //public int ProductId { get; set; }

        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
