using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FashionStore.Business.CartService;

public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;
    private const string CartSessionKey = "Cart";

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _session = _httpContextAccessor.HttpContext.Session;
    }

    public Cart GetCart()
    {
        var cartData = _session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(cartData))
        {
            return new Cart();
        }

        return JsonConvert.DeserializeObject<Cart>(cartData);
    }

    public void SaveCart(Cart cart)
    {
        var cartData = JsonConvert.SerializeObject(cart);
        _session.SetString(CartSessionKey, cartData);
    }

    public void AddToCart(Product product, int quantity)
    {
        var cart = GetCart();
        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity
            });
        }

        SaveCart(cart);
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem != null)
        {
            cart.Items.Remove(existingItem);
            SaveCart(cart);
        }
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem != null && quantity > 0)
        {
            existingItem.Quantity = quantity;
            SaveCart(cart);
        }
    }

    public void ClearCart()
    {
        _session.Remove(CartSessionKey);
    }
}
