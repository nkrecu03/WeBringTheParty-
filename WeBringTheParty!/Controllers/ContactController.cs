using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // ─────────────────────────────────────────────────
        // PUBLIC — Contact Form
        // ─────────────────────────────────────────────────

        [HttpGet]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Phone = HttpContext.Session.GetString("Phone");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string firstName, string lastName,
            string email, string phone, string subject, string message)
        {
            var contactMessage = new ContactMessageModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Subject = subject,
                Message = message,
                Status = "Unread",
                SentAt = DateTime.UtcNow
            };

            await _contactService.CreateMessageAsync(contactMessage);

            TempData["Success"] = "Thanks for reaching out! We'll get back to you within 24 hours.";
            return RedirectToAction("Index");
        }

        // ─────────────────────────────────────────────────
        // ADMIN — Messages List
        // ─────────────────────────────────────────────────

        public async Task<IActionResult> AdminMessages()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            var messages = await _contactService.GetAllMessagesAsync();
            return View(messages);
        }

        // ─────────────────────────────────────────────────
        // ADMIN — View Single Message
        // ─────────────────────────────────────────────────

        public async Task<IActionResult> ViewMessage(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            var message = await _contactService.GetMessageByIdAsync(id);
            if (message == null) return NotFound();

            // Auto-mark as Read when opened
            if (message.Status == "Unread")
                await _contactService.UpdateStatusAsync(id, "Read");

            return View(message);
        }

        // ─────────────────────────────────────────────────
        // ADMIN — Update Status
        // ─────────────────────────────────────────────────

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            await _contactService.UpdateStatusAsync(id, status);
            return RedirectToAction("ViewMessage", new { id });
        }

        // ─────────────────────────────────────────────────
        // ADMIN — Delete Message
        // ─────────────────────────────────────────────────

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            await _contactService.DeleteMessageAsync(id);
            return RedirectToAction("AdminMessages");
        }
    }
}