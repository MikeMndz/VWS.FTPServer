using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementUI.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RoleController : Controller
    {
        private readonly IRbacService _rbacService;

        public RoleController(IRbacService rbacService)
        {
            _rbacService = rbacService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _rbacService.GetRolesAsync();
            return View(models);
        }

        public async Task<IActionResult> AddOrEdit(int id)
        {
            ViewBag.PageName = id == 0 ? "Create Role" : "Edit Role";
            ViewBag.ActionName = id == 0 ? "Create" : "Edit";
            ViewBag.IsEdit = id == 0 ? false : true;
 
            if (id == 0)
                return View();
            else
            {
                var model = await _rbacService.GetRoleAsync(id);
                if (model == null)
                    return NotFound();

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,Enabled,IsStatic")] Role role)
        {
            if (ModelState.IsValid)
            {
                if (id != 0)
                    await _rbacService.UpdateRoleAsync(role);
                else
                    await _rbacService.CreateRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var model = await _rbacService.GetRoleAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecDelete(int id)
        {
            await _rbacService.DeleteRoleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
