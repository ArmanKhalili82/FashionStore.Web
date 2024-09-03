using FashionStore.Business.ProductsService;
using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using FashionStore.Web.Models;
using FashionStore.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FashionStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductsService _productService;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IProductsService productService, ApplicationDbContext context)
        {
            _logger = logger;
            _productService = productService;
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> ProductDetails(int id)
        {
            List<Product> products = new List<Product>();
            var product = await _productService.GetProductById(id);

            products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }

            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [ActionName("Remove")]
        public async Task<IActionResult> RemoveToCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            return View(products);
        }
    }
}
