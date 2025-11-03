using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryVerificationProject.Data;
using JewelleryVerificationProject.Models;
using ClosedXML.Excel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace JewelleryVerificationProject.Controllers
{
    public class JewelleryRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public JewelleryRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Display Records Page
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string branch, string certType, int? pieces, int? recordCount, int page = 1)
        {
            var query = _context.Jewellery.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(j => j.CertificateEnterDate >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(j => j.CertificateEnterDate <= toDate.Value);
            if (!string.IsNullOrEmpty(branch))
                query = query.Where(j => j.CompanyCode == branch);
            if (!string.IsNullOrEmpty(certType))
                query = query.Where(j => j.CertificateType == certType);
            if (pieces.HasValue)
                query = query.Where(j => j.BagQN == pieces.Value);

            int totalRecords = await query.CountAsync();
            int pageSize = recordCount ?? 10;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var data = await query
                .OrderByDescending(j => j.CertificateEnterDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
            ViewBag.Branch = branch;
            ViewBag.CertType = certType;
            ViewBag.Pieces = pieces;
            ViewBag.RecordCount = recordCount;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(data);
        }

        // ✅ Export to Excel using ClosedXML (no license needed)
        [HttpGet]
        public async Task<IActionResult> ExportToExcel(DateTime? fromDate, DateTime? toDate, string branch, string certType, int? pieces, int? recordCount)
        {
            var query = _context.Jewellery.AsQueryable();

            // Apply filters
            if (fromDate.HasValue)
                query = query.Where(j => j.CertificateEnterDate >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(j => j.CertificateEnterDate <= toDate.Value);
            if (!string.IsNullOrEmpty(branch))
                query = query.Where(j => j.CompanyCode == branch);
            if (!string.IsNullOrEmpty(certType))
                query = query.Where(j => j.CertificateType == certType);
            if (pieces.HasValue)
                query = query.Where(j => j.BagQN == pieces.Value);

            // ✅ Export all filtered data if recordCount is "All"
            if (recordCount.HasValue && recordCount.Value > 0)
                query = query.Take(recordCount.Value);

            var data = await query.ToListAsync();

            if (!data.Any())
            {
                TempData["ErrorMessage"] = "⚠️ No records found to export!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Jewellery Records");

                // ✅ Fix for invalid date ("Not a legal OleAut date")
                foreach (var item in data)
                {
                    if (item.CertificateEnterDate.Year < 1900)
                        item.CertificateEnterDate = new DateTime(1900, 1, 1);
                }

                // ✅ Insert full data (keep original property names)
                worksheet.Cell(1, 1).InsertTable(data);

                worksheet.Columns().AdjustToContents(); // Auto-fit

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                string fileName = $"JewelleryRecords_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "❌ Excel export failed: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
