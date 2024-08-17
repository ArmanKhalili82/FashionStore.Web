using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class WishlistItem
{
    public int Id { get; set; }
    [DisplayName("Wishlist")]
    public int WishlistId { get; set; }
    [ForeignKey("WishlistId")]
    public Wishlist Wishlist { get; set; }
    [DisplayName("Product Name")]
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}
