using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class CartItem
{
    public int Id { get; set; }
    [DisplayName("Cart")]
    public int CartId { get; set; }
    [ForeignKey("CartId")]
    public Cart Cart { get; set; }
    [DisplayName("Product Name")]
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
