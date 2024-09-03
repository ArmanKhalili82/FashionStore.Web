using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.OrderService;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailSender _emailSender;

    public OrderService(ApplicationDbContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    public async Task CreateOrder(Order order)
    {
        await _context.orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(int orderId)
    {
        var order = await _context.orders.FindAsync(orderId);
        _context.orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _context.orders.FindAsync(id);
        return order;
    }

    public async Task<Order> GetOrderByName(string name)
    {
        var order = await _context.orders.FindAsync(name);
        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _context.orders.ToListAsync();
        return orders;
    }

    public async Task<OrderResult> PlaceOrder(int productId, int quantity)
    {
        var product = await _context.products.FindAsync(productId);
        if (product == null)
        {
            return OrderResult.ProductNotFound;
        }

        // Check if there is enough stock
        if (product.StockQuantity < quantity)
        {
            return OrderResult.NotEnoughStock;
        }

        // Deduct the quantity from the stock
        product.StockQuantity -= quantity;
        // Check stock status and handle notifications
        if (product.StockQuantity == 0)
        {
            NotifyOutOfStock(product);
        }
        else if (product.IsLowOnStock)
        {
            NotifyLowOnStock(product);
        }

        // Save the changes to the database
        await _context.SaveChangesAsync();

        return OrderResult.Success;
    }

    private async void NotifyOutOfStock(Product product)
    {
        // Define the recipient email address (e.g., admin or supplier)
        string adminEmail = "admin@fashionstore.com";

        // Create the email content
        string subject = $"Product Out of Stock: {product.Name}";
        string message = $"The product '{product.Name}' is now out of stock.\n\n" +
                         "Please restock this item as soon as possible.";

        // Send the email notification
        await _emailSender.SendEmailAsync(adminEmail, subject, message);
    }

    private async void NotifyLowOnStock(Product product)
    {
        // Define the recipient email address (e.g., admin or store manager)
        string adminEmail = "admin@fashionstore.com";

        // Create the email content
        string subject = $"Low Stock Alert: {product.Name}";
        string message = $"The product '{product.Name}' is running low on stock.\n" +
                         $"Only {product.StockQuantity} units are left in inventory.\n\n" +
                         "Consider restocking soon to avoid running out of stock.";

        // Send the email notification
        await _emailSender.SendEmailAsync(adminEmail, subject, message);
    }

    public async Task UpdateOrder(Order order)
    {
        _context.orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public enum OrderResult
    {
        Success,
        ProductNotFound,
        NotEnoughStock
    }
}
