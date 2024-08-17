using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class Review
{
    public int Id { get; set; }
    [DisplayName("Product Name")]
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [DisplayName("User Name")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int Rating { get; set; } // e.g., 1 to 5 stars
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
}
