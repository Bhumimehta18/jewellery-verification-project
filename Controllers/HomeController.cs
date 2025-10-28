using Microsoft.AspNetCore.Mvc;

namespace JewelleryVerificationProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
