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
       

        [HttpGet]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await userService.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserModel editedUser)
        {
            if (!ModelState.IsValid)
            {
                return View(editedUser);
            }
            try
            {
                await userService.EditUserAsync(editedUser);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(editedUser);
            }
        }
    }
}
