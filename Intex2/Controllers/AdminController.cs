using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Intex2.Controllers
{
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roleExist = 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole role)
        {
            var roleExist = await roleManager.RoleExistsAsync(role.RoleName);

            if(!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole)
            }
            return View();
        }
    }
}
