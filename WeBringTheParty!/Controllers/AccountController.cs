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

            //verify email and password is valid for logging in
            if (user == null || !user.isActive)
            {
                ViewBag.Error = "Invalid email or password";
                return View(model);
            }

            //set user's data for login session
            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("LastName", user.LastName);
            HttpContext.Session.SetString("Email", user.EmailAddress);
            HttpContext.Session.SetString("Phone", user.PhoneNumber ?? "");
            HttpContext.Session.SetString("UserID", user.UserID.ToString());
            HttpContext.Session.SetString("Role", user.Role);

            //if user is admin, send to admin dashboard
            //send customers to welcome page
            if (user.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return RedirectToAction("Welcome");
            }
        }


        public IActionResult Welcome()
        {
            var firstName = HttpContext.Session.GetString("FirstName");

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
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
