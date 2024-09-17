using Microsoft.AspNetCore.Mvc;

namespace Finders.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            // Check if the user is authenticated before showing the dashboard
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}
