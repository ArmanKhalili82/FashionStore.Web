using FashionStore.Business.CartService;
using FashionStore.Business.ProductsService;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductsService _productService;
    private readonly ICartService _cartService;

    public CartController(IProductsService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity)
    {
        var product = await _productService.GetProductById(productId);

        if (product != null)
        {
            _cartService.AddToCart(product, quantity);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        _cartService.UpdateQuantity(productId, quantity);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ClearCart()
    {
        _cartService.ClearCart();
        return RedirectToAction("Index");
    }
}
