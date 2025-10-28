using Microsoft.AspNetCore.Mvc;

namespace JewelleryVerificationProject.Controllers
{
    public class AccountController : Controller
    {
        // Show welcome/login page
        public IActionResult Welcome()
        {
            return View();
        }

        // Handle login form
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // ✅ Simple check (replace with real authentication later)
            if (username == "admin" && password == "12345")
            {
                // Redirect to home
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password!";
            return View("Welcome");
        }

        public IActionResult Logout()
        {
            // Clear authentication later if you add Identity
            return RedirectToAction("Welcome");
        }
    }
}
