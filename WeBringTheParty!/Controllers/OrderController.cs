using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class OrderController : Controller 
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        // GET: Order/Checkout - The confirmation page
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            var cartItems = await _cartService.GetCartByUserIdAsync(userId);

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(cartItems);
        }

        // POST: Order/PlaceOrder - The final purchase
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            try
            {
                // This call saves the order, updates inventory, and clears the cart
                await _orderService.PlaceOrderAsync(userId);
                TempData["SuccessMessage"] = "Thank you! Your party order has been placed.";
                return RedirectToAction("History");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Checkout");
            }
        }

        // GET: Order/History - View past orders
        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            int userId = userIdString != null ? int.Parse(userIdString) : 0;

            var orders = await _orderService.GetUserOrderHistoryAsync(userId);
            return View(orders);
        }
    }
}
