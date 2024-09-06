using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.CartService;

public interface ICartService
{
    Cart GetCart();
    void AddToCart(Product product, int quantity);
    void RemoveFromCart(int productId);
    void UpdateQuantity(int productId, int quantity);
    void ClearCart();
}
