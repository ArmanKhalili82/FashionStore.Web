using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class CheckoutViewModel
{
    public string CustomerName { get; set; }

    [EmailAddress]
    public string CustomerEmail { get; set; }

    [Required]
    public string ShippingAddress { get; set; }

    public List<CartItem> CartItems { get; set; }
    public decimal TotalAmount { get; set; }
}
