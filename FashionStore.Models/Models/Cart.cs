using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class Cart
{
    public int Id { get; set; }
    [DisplayName("User Name")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public decimal TotalPrice => Items.Sum(item => item.Product.Price * item.Quantity);
}
