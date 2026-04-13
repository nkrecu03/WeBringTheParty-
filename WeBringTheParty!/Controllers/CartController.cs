using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WeBringTheParty_.Controllers
{
  
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            await _cartService.AddToCartAsync(userId, productId, quantity);

            return RedirectToAction("Index"); // Refresh the cart page
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;
            await _cartService.RemoveFromCartAsync(userId, cartItemId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get the logged-in User's ID
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            // Fetch items from the BLL
            var cartItems = await _cartService.GetCartByUserIdAsync(userId);

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            try
            {
                await _orderService.PlaceOrderAsync(userId);
                return RedirectToAction("History"); // Redirect to history after success
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            var orders = await _orderService.GetUserOrderHistoryAsync(userId);
            return View(orders); //
        }
    }
}
