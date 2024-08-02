using Interfaces;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApp.Extensions;
using ViewModels;

namespace PizzeriaApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _productService.GetProductByIdAsync(id).Result;
            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart") ?? new List<ProductViewModel>();
            cart.Add(product);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart") ?? new List<ProductViewModel>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("Cart");
            if (cart != null)
            {
                var item = cart.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }
            return RedirectToAction("Cart");
        }
    }
}
