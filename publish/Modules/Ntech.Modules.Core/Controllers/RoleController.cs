using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ntech.Modules.Core.Models;
using Ntech.Modules.Core.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ntech.Modules.Core.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> roleManager;

        public RoleController(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new List<RoleViewModel>();
            model = roleManager.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.Name,
                NormalizedName = r.NormalizedName
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            var model = new RoleViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.NormalizedName = applicationRole.NormalizedName;
                }
            }
            return PartialView("_AddEditApplicationRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !string.IsNullOrEmpty(id);
                var applicationRole = isExist ? await roleManager.FindByIdAsync(id) : new Role();
                applicationRole.Name = model.RoleName;
                applicationRole.NormalizedName = model.NormalizedName;
                var roleRuslt = isExist ? await roleManager.UpdateAsync(applicationRole)
                                                    : await roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
