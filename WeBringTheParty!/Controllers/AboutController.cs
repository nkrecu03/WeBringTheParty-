using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}