using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionStore.Models.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a positive number")]
        public int StockQuantity { get; set; }
        public bool IsOutOfStock => StockQuantity <= 0; // Checks if the product is out of stock
        public bool IsLowOnStock => StockQuantity > 0 && StockQuantity <= LowStockThreshold;

        public const int LowStockThreshold = 5; // Defines when the stock is considered low
        public int SalesCount { get; set; }
        public bool IsFeatured {  get; set; }

        public string? ImageUrl { get; set; }

        //[DisplayName("Sub Category")]
        //public int SubCategoryId { get; set; }

        //[ForeignKey("SubCategoryId")]

        public ICollection<Size> Sizes { get; set; }

        public ICollection<Color> Colors { get; set; }
    }
}
