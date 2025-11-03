using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JewelleryVerificationProject.Controllers
{
    // ✅ Custom attribute to protect pages (checks if user is logged in)
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(session))
            {
                // Redirect to login page if no session found
                context.Result = new RedirectToActionResult("Welcome", "Account", null);
            }
            base.OnActionExecuting(context);
        }
    }

    public class AccountController : Controller
    {
        // ✅ Show login page
        public IActionResult Welcome()
        {
            return View();
        }

        // ✅ Handle login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Simple dummy authentication (you can link DB later)
            if (username == "admin" && password == "12345")
            {
                // ✅ Save session
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password!";
            return View("Welcome");
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // remove session
            return RedirectToAction("Welcome", "Account"); // back to login
        }
    }
}
