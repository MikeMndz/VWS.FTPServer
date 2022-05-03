using ApplicationCore.Definitions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementUI.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRbacService _rbacService;

        public UserController(IUserService userService, IRbacService rbacService)
        {
            _userService = userService;
            _rbacService = rbacService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _userService.GetAllAsync();
            return View(models);
        }

        public async Task<IActionResult> AddOrEdit(int id)
        {
            ViewBag.PageName = id == 0 ? "Create User" : "Edit User";
            ViewBag.ActionName = id == 0 ? "Create" : "Edit";
            ViewBag.IsEdit = id == 0 ? false : true;
            var roleRecords = await _rbacService.GetRolesAsync();
            var roles = new List<SelectListItem>();
            foreach (var role in roleRecords)
                roles.Add(new SelectListItem() { Text = role.Name, Value = role.Id + "" });

            ViewBag.Roles = roles;

            if (id == 0)
                return View();
            else
            {
                var model = await _userService.GetAsync(id);
                if (model == null)
                    return NotFound();
                
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Account,Password,Name,LastName,MothersLastName,Enabled,IdRole")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                if (userData.Id != 0)
                    await _userService.UpdateAsync(userData);
                else
                    await _userService.CreateAsync(userData);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var model = await _userService.GetAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecDelete(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
