using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeinerSales.Models;
using WeinerSales.ViewModels;
using WeinerSales.Data;


namespace WeinerSales.Controllers
{
    public class AccountController : Controller
    {
        private readonly WeinerDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, WeinerDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

            //if (!_db.Roles.Any(r => r.Name == "SalesAssociate"))
            //    _db.Roles.Add(new IdentityRole() { Name = "SalesAssociate" });
            //if (!_db.Roles.Any(r => r.Name == "Manager"))
            //    _db.Roles.Add(new IdentityRole() { Name = "Manager" });
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            AppUser user = new AppUser { UserName = model.UserName, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                return View();
        }
    }
}
