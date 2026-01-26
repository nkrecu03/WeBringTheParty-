using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;


namespace WeBringTheParty_.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await userService.LoginAsync(model.Email, model.Password);

            if (user != null)
            {
                TempData["FirstName"] = user.FirstName;
                return RedirectToAction("Welcome");
            }

            ViewBag.Error = "Invalid email or password";
            return View(model);
        }

        public IActionResult Welcome()
        {
            var firstName = TempData["FirstName"] as string;

            if (string.IsNullOrEmpty(firstName))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.FirstName = firstName;
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    










        /*
        [HttpPost]
        public IActionResult Login(Models.LoginViewModel model)
        {
            return RedirectToAction("Welcome", new {username = model.Email });
        }

        [HttpPost]
        public IActionResult Logout() { 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Welcome(string email)
        {
            ViewBag.Username = email;
            return View();
        } */
    }
}
