using Microsoft.AspNetCore.Mvc;
using JewelleryVerificationProject.Data;
using JewelleryVerificationProject.Models;
using Microsoft.AspNetCore.Http;

namespace JewelleryVerificationProject.Controllers
{
    public class DataEntryController : Controller
    {
        private readonly AppDbContext _context;

        public DataEntryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // 🔒 Security Check
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Welcome", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Index(Jewellery model)
        {
            // 🔒 Security Check
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Welcome", "Account");

            if (ModelState.IsValid)
            {
                _context.Jewellery.Add(model);
                _context.SaveChanges();
                ViewBag.Message = "Data saved successfully!";
                return View();
            }

            return View(model);
        }
    }
}
