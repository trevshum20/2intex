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
        public IActionResult Index(Search search)
        {
            var blah = _context.utah_crashes_table
                .Where(x=>x.CITY.Contains(search.City))
                .Take(num_crashes).ToList();
            return View("Summary", blah);
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
        public IActionResult AboutML()
        {
            return View();
        }
    }
}









