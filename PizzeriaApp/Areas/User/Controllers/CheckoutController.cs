using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PizzeriaApp.Areas.User.Controllers
{
    [Area("User")]
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;

        public CheckoutController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.Name;
            var orders = _orderService.GetUserOrdersAsync(userId).Result;
            return View(orders);
        }
    }
}
