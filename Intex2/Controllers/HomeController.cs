using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Intex2.Models;
using Intex2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 10;

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
            
            if (search.Topic.Equals("City"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.CITY.Contains(search.SearchTerm))
                .Take(PageSize).ToList();
                return View("Summary", blah);
            }
            else if (search.Topic.Equals("County"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.COUNTY_NAME.Contains(search.SearchTerm))
                .Take(PageSize).ToList();
                return View("Summary", blah);
            }
            else if (search.Topic.Equals("CrashId"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.CRASH_ID.ToString().Contains(search.SearchTerm))
                .Take(PageSize).ToList();
                return View("Summary", blah);
            }
            else if (search.Topic.Equals("Road"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.MAIN_ROAD_NAME.Contains(search.SearchTerm))
                .Take(PageSize).ToList();
                return View("Summary", blah);
            }
            else
            {
                var blah = _context.utah_crashes_table
                .Take(PageSize).ToList();
                return View("Summary", blah);
            }

            
        }
        
        public IActionResult Summary(int crashPage = 1)
        {

            var blah = _context.utah_crashes_table
                .Take(10)
                .ToList();

            return View(new CrashListViewModel
            {
                Crashes = _context.utah_crashes_table
                    .OrderBy(c => c.CRASH_ID)
                    .Skip((crashPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = crashPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _context.utah_crashes_table.Count()
                }
            });
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









