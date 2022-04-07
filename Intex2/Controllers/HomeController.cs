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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchSummary(Search search, int crashPage = 1)
        {

            if (search.Topic.Equals("City"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.CITY.Contains(search.SearchTerm));

                return View(new CrashListViewModel
                {
                    Crashes = _context.utah_crashes_table
                                .Where(x => x.CITY.Contains(search.SearchTerm))
                                .OrderBy(c => c.CRASH_ID)
                                .Skip((crashPage - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = crashPage,
                        ItemsPerPage = PageSize,
                        TotalItems = blah.Count()
                    }
                });
            }
            else if (search.Topic.Equals("County"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.COUNTY_NAME.Contains(search.SearchTerm));
                return View(new CrashListViewModel
                {
                    Crashes = _context.utah_crashes_table
                                .Where(x => x.COUNTY_NAME.Contains(search.SearchTerm))
                                .OrderBy(c => c.CRASH_ID)
                                .Skip((crashPage - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = crashPage,
                        ItemsPerPage = PageSize,
                        TotalItems = blah.Count()
                    }
                });
            }
            else if (search.Topic.Equals("CrashId"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.CRASH_ID.ToString().Contains(search.SearchTerm));
                return View(new CrashListViewModel
                {
                    Crashes = _context.utah_crashes_table
                                .Where(x => x.CRASH_ID.ToString().Contains(search.SearchTerm))
                                .OrderBy(c => c.CRASH_ID)
                                .Skip((crashPage - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = crashPage,
                        ItemsPerPage = PageSize,
                        TotalItems = blah.Count()
                    }
                });
            }
            else if (search.Topic.Equals("Road"))
            {
                var blah = _context.utah_crashes_table
                .Where(x => x.MAIN_ROAD_NAME.Contains(search.SearchTerm));
                return View(new CrashListViewModel
                {
                    Crashes = _context.utah_crashes_table
                                .Where(x => x.MAIN_ROAD_NAME.Contains(search.SearchTerm))
                                .OrderBy(c => c.CRASH_ID)
                                .Skip((crashPage - 1) * PageSize)
                                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = crashPage,
                        ItemsPerPage = PageSize,
                        TotalItems = blah.Count()
                    }
                });
            }
            else
            {
                var blah = _context.utah_crashes_table;
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
        }

        public IActionResult Summary(int crashPage = 1)
        {

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

        [HttpGet]
        public IActionResult Delete(int CrashId)
        {
            var crash = _context.utah_crashes_table.Single(x => x.CRASH_ID == CrashId);
            return View(crash);
        }

        [HttpPost]
        public IActionResult Delete(utah_crashes_table deleteInfo)
        {
            _context.Remove(deleteInfo);
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









