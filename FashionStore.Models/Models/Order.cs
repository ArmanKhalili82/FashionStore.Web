using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Models.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string ShippingAddress { get; set; }
    [DisplayName("User Name")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
}
