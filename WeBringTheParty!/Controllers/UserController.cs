using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userService.GetUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel createdUser)
        {
            await userService.CreateUserAsync(createdUser);

            // set session
            HttpContext.Session.SetString("FirstName", createdUser.FirstName);
            HttpContext.Session.SetString("Role", createdUser.Role ?? "Customer"); 

            return RedirectToAction("Welcome", "Account");
        }
    }
}
