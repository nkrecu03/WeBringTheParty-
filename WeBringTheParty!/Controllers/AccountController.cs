using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login(Models.LoginViewModel model)
        {
            return RedirectToAction("Welcome", new {username = model.Username });
        }

        [HttpPost]
        public IActionResult Logout() { 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Welcome(string username)
        {
            ViewBag.Username = username;
            return View();
        }
    }
}
