using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Intex2.Models;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        int num_crashes = 10;

        private CrashDbContext _context { get; set; }
        public HomeController(CrashDbContext temp)
        {
            _context = temp;
        }
        public DbSet<utah_crashes_table> utah_crashes_table { get; set; }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Search search)
        {
            var results = from m in _context.utah_crashes_table
                         select m;

            if (!String.IsNullOrEmpty(search.City))
            {
                results = results.Where(s => s.CITY!.Contains(search.City));
            }

            return View("SearchResult", await results.ToListAsync());
        }
        //public IActionResult Index(Search search)
        //{
        //    var results = (from r in _context.utah_crashes_table
        //                  select r);
        //    //var results = _context.utah_crashes_table.Take(100000).ToList();

        //    if (!String.IsNullOrEmpty(search.City))
        //    {
        //        results = results.Where(r => r.CITY.Contains(search.City));
        //    }
        //    if (!String.IsNullOrEmpty(search.County))
        //    {
        //        results = results.Where(r => r.COUNTY_NAME.Contains(search.County));
        //    }
        //    if (search.CrashId != 0)
        //    {
        //        results = results.Where(r => r.CRASH_ID == search.CrashId);
        //    }
        //    if (search.Severity != 0)
        //    {
        //        results = results.Where(r => r.CRASH_SEVERITY_ID == search.Severity);
        //    }
        //    //results.ToList();
        //    return View("SearchResult", results);
        //}
        public IActionResult SearchResult()
        {
            return View();
        }
        public IActionResult Summary()
        {
            var blah = _context.utah_crashes_table.Take(num_crashes).ToList();
            return View(blah);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(utah_crashes_table uc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uc);
                _context.SaveChanges();
                return View("Confirmation", uc);
            }
            else
            {
                return View(uc);
            }
        }
        [HttpGet]
        public IActionResult Edit(int CrashId)
        {
            var crash = _context.utah_crashes_table.Single(x => x.CRASH_ID == CrashId);
            return View("Edit", crash);
        }
        [HttpPost]
        public IActionResult Edit(utah_crashes_table editInfo)
        {
            _context.Update(editInfo);
            _context.SaveChanges();
            return RedirectToAction("Summary");
        }
        public IActionResult Details(int CrashId)
        {
            var crash = _context.utah_crashes_table.Single(x => x.CRASH_ID == CrashId);
            return View("Details", crash);
        }
        public IActionResult Delete(int CrashId)
        {
            var crash = _context.utah_crashes_table.Single(x => x.CRASH_ID == CrashId);
            _context.Remove(crash);
            _context.SaveChanges();
            return RedirectToAction("Summary");
        }
        public IActionResult Analysis()
        {
            return View();
        }
    }
}









