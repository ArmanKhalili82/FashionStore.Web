using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FashionStore.Business.OrderService.OrderService;

namespace FashionStore.Business.OrderService;

public interface IOrderService
{
    Task<List<Order>> GetOrders();
    Task<Order> GetOrderById(int id);
    Task<Order> GetOrderByName(string name);
    Task<OrderResult> PlaceOrder(int productId, int quantity);
    Task CreateOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(int orderId);
}
