using Microsoft.AspNetCore.Mvc;
using JewelleryVerificationProject.Data;
using JewelleryVerificationProject.Models;

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
            return View();
        }

        [HttpPost]
        public IActionResult Index(Jewellery model)
        {
            if (ModelState.IsValid)
            {
                _context.Jewellery.Add(model);
                _context.SaveChanges();
                ViewBag.Message = "Data saved successfully!";
                return View(); // reloads form after saving
            }

            return View(model); // shows validation errors if any
        }
    }
}
