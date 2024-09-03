using FashionStore.Business.OrderService;
using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using FashionStore.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using static FashionStore.Business.OrderService.OrderService;

namespace FashionStore.Web.Controllers;

public class OrderController : Controller
{
    private IOrderService _orderService;
    private readonly ApplicationDbContext _context;

    public OrderController(IOrderService orderService, ApplicationDbContext context)
    {
        _orderService = orderService;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();
        return View(orders);
    }

    [HttpGet]
    public IActionResult CreateOrder()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(int productId, int quantity)
    {
        var result = await _orderService.PlaceOrder(productId, quantity);

        switch (result)
        {
            case OrderResult.Success:
                TempData["Message"] = "Order placed successfully!";
                return RedirectToAction("Index", "Home");

            case OrderResult.ProductNotFound:
                TempData["Error"] = "Product not found!";
                return RedirectToAction("Index", "Home");
            case OrderResult.NotEnoughStock:
                TempData["Error"] = "Not enough stock available!";
                return RedirectToAction("Index", "Home");

            default:
                TempData["Error"] = "An error occurred while placing the order.";
                return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditOrder(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> EditOrder(Order order)
    {
        await _orderService.UpdateOrder(order);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return View(order);
    }

    [HttpPost, ActionName("DeleteOrder")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderService.DeleteOrder(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(Order order)
    {
        List<Product> products = HttpContext.Session.Get<List<Product>>("products");
        if (products != null)
        {
            foreach (var product in products)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ProductId = product.Id;
                order.OrderItems.Add(orderItem);
            }
        }

        order.OrderNo = GetOrderNo();
        await _context.orders.AddAsync(order);
        await _context.SaveChangesAsync();
        HttpContext.Session.Set("products", new List<Product>());

        return View();
    }

    public string GetOrderNo()
    {
        int rowCount = _context.orders.ToList().Count() + 1;
        return rowCount.ToString("000");
    }
}
