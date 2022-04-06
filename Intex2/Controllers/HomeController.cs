﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Intex2.Models;
using Intex2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using PagedList;

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
    }
}









