using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET /Rental/Index — catalog page
        public async Task<IActionResult> Index()
        {
            var items = await _rentalService.GetAllRentalItemsAsync();
            return View(items);
        }

        // GET /Rental/CheckAvailability/5
        public async Task<IActionResult> CheckAvailability(int id, int? year, int? month)
        {
            var item = await _rentalService.GetRentalItemByIdAsync(id);
            if (item == null) return NotFound();

            int y = year ?? DateTime.Today.Year;
            int m = month ?? DateTime.Today.Month;

            var unavailableSlots = await _rentalService.GetAvailabilityCalendarAsync(id, y, m);

            ViewBag.Year = y;
            ViewBag.Month = m;
            ViewBag.UnavailableSlots = unavailableSlots;
            ViewBag.AllTimeSlots = new List<string> { "Morning (8AM–12PM)", "Afternoon (12PM–5PM)", "Full Day (8AM–5PM)" };

            return View(item);
        }

        // POST /Rental/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int rentalItemId, string rentalDate, string timeSlot)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);
            DateTime date = DateTime.Parse(rentalDate);

            bool success = await _rentalService.BookRentalAsync(rentalItemId, userId, date, timeSlot);

            if (success)
                TempData["Success"] = "Rental booked! We'll contact you to confirm delivery.";
            else
                TempData["Error"] = "That slot is no longer available. Please choose another.";

            return RedirectToAction("CheckAvailability", new { id = rentalItemId });
        }

        // ─────────────────────────────────────────────────
        // ADMIN — INDEX
        // ─────────────────────────────────────────────────

        public async Task<IActionResult> AdminIndex()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            var items = await _rentalService.GetAllRentalItemsAsync();
            return View(items);
        }

        // ─────────────────────────────────────────────────
        // ADMIN — CREATE
        // ─────────────────────────────────────────────────

        [HttpGet]
        public IActionResult CreateRentalItem()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRentalItem(RentalItemModel model, IFormFile ImageFile)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_images");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                model.ImageUrl = fileName;
            }

            await _rentalService.CreateRentalItemAsync(model);
            return RedirectToAction("AdminIndex");
        }

        // ─────────────────────────────────────────────────
        // ADMIN — EDIT
        // ─────────────────────────────────────────────────

        [HttpGet]
        public async Task<IActionResult> EditRentalItem(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            var item = await _rentalService.GetRentalItemByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRentalItem(RentalItemModel model, IFormFile ImageFile)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_images");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                model.ImageUrl = fileName;
            }

            await _rentalService.EditRentalItemAsync(model);
            return RedirectToAction("AdminIndex");
        }

        // ─────────────────────────────────────────────────
        // ADMIN — DELETE
        // ─────────────────────────────────────────────────

        [HttpGet]
        public async Task<IActionResult> DeleteRentalItem(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            var item = await _rentalService.GetRentalItemByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("DeleteRentalItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            await _rentalService.DeleteRentalItemAsync(id);
            return RedirectToAction("AdminIndex");
        }
    }
}


