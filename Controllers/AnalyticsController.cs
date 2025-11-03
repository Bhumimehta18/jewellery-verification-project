using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryVerificationProject.Data;
using System;
using System.Linq;

namespace JewelleryVerificationProject.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Certificate type counts
            var certificateTypeCounts = _context.Jewellery
                .GroupBy(j => j.CertificateType)
                .Select(g => new { Type = g.Key ?? "Unknown", Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // Total unique customers
            var totalCustomers = _context.Jewellery
                .Select(j => j.Customer)
                .Distinct()
                .Count();

            // Yearly certificate counts
            var yearlyCounts = _context.Jewellery
                .GroupBy(j => j.CertificateEnterDate.Year)
                .Select(g => new { Year = g.Key, Count = g.Count() })
                .OrderBy(x => x.Year)
                .ToList();

            ViewBag.CertificateTypeCounts = certificateTypeCounts;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.YearlyCounts = yearlyCounts;

            return View();
        }

        // ✅ New Action → Display unique customers
        public IActionResult Customers(string search)
        {
            var customers = _context.Jewellery
                .Select(j => j.Customer)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            if (!string.IsNullOrWhiteSpace(search))
                customers = customers.Where(c => c.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Search = search;
            return View("CustomerList", customers);
        }
    }
}
