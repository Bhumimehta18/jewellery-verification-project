using Microsoft.AspNetCore.Mvc;

namespace JewelleryVerificationProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // 🔒 Security Check
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Welcome", "Account");

            return View();
        }
    }
}
