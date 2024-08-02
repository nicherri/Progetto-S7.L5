using Interfaces;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApp.Extensions;
using ViewModels;

namespace PizzeriaApp.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart") ?? new List<ProductViewModel>();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _productService.GetProductByIdAsync(id).Result;
            if (product == null)
                return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart") ?? new List<ProductViewModel>();
            cart.Add(product);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index", "Home", new { area = "User" });
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart");
            if (cart != null)
            {
                var product = cart.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    cart.Remove(product);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart") ?? new List<ProductViewModel>();
            if (cart.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var order = new OrderViewModel
            {
                ProdottiOrdinati = cart,
                UserId = User.Identity.Name
            };

            _orderService.CreateOrderAsync(order);

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index", "Checkout", new { area = "User" });
        }
    }
}
