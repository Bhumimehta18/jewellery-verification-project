using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Jewellery_Verification_Project.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Entry()
        {
            // 🔒 Security Check
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Welcome", "Account");

            return View();
        }
    }
}
