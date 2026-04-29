using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IRentalService _rentalService;

        public HomeController(IProductService productService, IRentalService rentalService)
        {
            _productService = productService;
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            // Best sellers
            var bestSellers = (await _productService.GetBestSellersAsync(4)).ToList();
            if (!bestSellers.Any())
            {
                var all = await _productService.GetProductsAsync();
                bestSellers = all.Where(p => p.isActive).Take(4).ToList();
            }

            // Most rented
            var mostRented = (await _rentalService.GetMostRentedAsync(3)).ToList();

            ViewBag.MostRented = mostRented;

            return View(bestSellers);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}