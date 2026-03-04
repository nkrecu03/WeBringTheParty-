using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WeBringTheParty_.Controllers
{
  
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            // Get the logged-in User's ID (assuming you're using Identity/Claims)
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _cartService.AddToCartAsync(userId, productId, quantity);

            return RedirectToAction("Index"); // Refresh the cart page
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.RemoveFromCartAsync(userId, cartItemId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get the logged-in User's ID
            int userId = 13;

            // Fetch items from the BLL
            var cartItems = await _cartService.GetCartByUserIdAsync(userId);

            return View(cartItems);
        }
    }
}
