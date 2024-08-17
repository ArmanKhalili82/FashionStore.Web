using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionStore.Models.Models
{
    public class Size
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Size")]
        public ProductSize ProductSize { get; set; }

        [DisplayName("Product Name")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }

    public enum ProductSize
    {
        [Display(Name = "Small")]
        Small,

        [Display(Name = "Medium")]
        Medium,

        [Display(Name = "Large")]
        Large,

        [Display(Name = "X-Large")]
        XLarge,

        [Display(Name = "XX-Large")]
        XXLarge
    }
}
