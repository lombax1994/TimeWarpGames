using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TimeWarpGames.Webapp.Models;

namespace TimeWarpGames.Webapp.Controllers
{
    public class RolesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        [Authorize(Roles="SuperAdmin")]
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = new IdentityRole()
            {
                Name = collection["Name"]
            };
            var identityResult = roleManager.Create(role);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessages = identityResult.Errors;
            return View();
        }

        public ActionResult Users()
        {
            var users = db.Users.OrderBy(x => x.UserName).ToList();
            return View(users);
        }

        [HttpPost]
        public ActionResult ManageUserRoles(FormCollection collection)
        {
            var model = new UserRole();
            var userName = collection["UserName"];
            var identityUser = db.Users.FirstOrDefault(x => x.UserName == userName.Trim());
            if (identityUser != null)
            {
                model.UserId = identityUser.Id;
                model.UserName = identityUser.UserName;
                var identityRoles = db.Roles.ToList();
                var roleIds = identityUser.Roles.Select(x => x.RoleId).ToList();
                foreach (var identityRole in identityRoles)
                {
                    model.UserRoles.Add(new Role()
                    {
                        Id = identityRole.Id,
                        Name = identityRole.Name,
                        IsChecked = roleIds.Contains(identityRole.Id)
                    });
                }
            }
            else
            {
                model = null;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateRoles(UserRole user)
        {
            var userManager = new UserManager<ApplicationUser>(new
                UserStore<ApplicationUser>(db));
            foreach (var role in user.UserRoles)
            {
                if (role.IsChecked)
                {
                    if (!userManager.IsInRole(user.UserId, role.Name))
                    {
                        userManager.AddToRole(user.UserId, role.Name);
                    }
                }
                else
                {
                    if (userManager.IsInRole(user.UserId, role.Name))
                    {
                        userManager.RemoveFromRole(user.UserId, role.Name);
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}