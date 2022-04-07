// group 3-10, 4/7/22

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Intex2.Controllers
{
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager; // create role manager

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager; // instantiate it in the constructor
        }



        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")] // restrict creating of new roles to admins
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [Authorize(Roles = "Admin")] // allows admin to create new roles.
        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole role)
        {
            var roleExist = await roleManager.RoleExistsAsync(role.RoleName);

            if(!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return View();
        }
    }
}
