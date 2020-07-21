using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using MyBookStore.Areas.Identity.Data;
using MyBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController: Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<ApplicationUser> UserManager { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            //roleManager.CreateAsync(new IdentityRole("CanManageMovie"));
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> CreateRole(CreateRoleViewModel role)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {

                    Name = role.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles","Administartion");
                }

                foreach(IdentityError errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }
            return View(role);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;


            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
          var role = await roleManager.FindByIdAsync(Id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                Name = role.Name,

            };
            foreach(var user in UserManager.Users)
            {
               if(await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.Name;
               var result =  await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        
        }
        
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.Id = id;

            var role = await roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");

            }
            var model = new List<UserRoleViewModel>();

            foreach (var users in UserManager.Users)
            {

                var userRoleModeView = new UserRoleViewModel
                {
                    UserId = users.Id,
                    UserName = users.LastName
                };

                if(await UserManager.IsInRoleAsync(users, role.Name))
                {
                    userRoleModeView.isSelected = true;
                }
                else
                {
                    userRoleModeView.isSelected = false;
                }
                model.Add(userRoleModeView);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model,string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }
            for(int i = 0; i < model.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(model[i].UserId);
                IdentityResult result= null;

                if (model[i].isSelected && !(await UserManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }else if(!(model[i].isSelected) && await UserManager.IsInRoleAsync(user, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(user,role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if(i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = Id });
                    }

                }

            }

            return RedirectToAction("EditRole", new { Id = Id });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }


}
