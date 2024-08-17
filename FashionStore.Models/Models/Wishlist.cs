using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class Wishlist
{
    public int Id { get; set; }
    [DisplayName("User Name")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public ICollection<WishlistItem> WishlistItems { get; set; }
}
