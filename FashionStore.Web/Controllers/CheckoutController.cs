using FashionStore.Business.CartService;
using FashionStore.Business.OrderService;
using FashionStore.Models.Models;
using FashionStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            var model = new CheckoutViewModel
            {
                CartItems = cart.Items,
                TotalAmount = cart.TotalPrice
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cart = _cartService.GetCart();
                model.CartItems = cart.Items;
                model.TotalAmount = cart.TotalPrice;
                return View("Index", model);
            }

            var order = new Order
            {
                CustomerName = model.CustomerName,
                CustomerEmail = model.CustomerEmail,
                ShippingAddress = model.ShippingAddress,
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                OrderItems = model.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList()
            };

            await _orderService.CreateOrder(order);
            _cartService.ClearCart(); // Clear cart after order is placed

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            return View(orderId);
        }
    }
}
