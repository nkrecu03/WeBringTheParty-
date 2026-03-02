using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WeBringTheParty_.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                // Redirect non-admin users
                return RedirectToAction("Index", "Home");
            }

            return View(); // Returns Views/Admin/Dashboard.cshtml
        }
    }
}
