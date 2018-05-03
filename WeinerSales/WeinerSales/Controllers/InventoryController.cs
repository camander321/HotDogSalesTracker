using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeinerSales.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeinerSales.Controllers
{
    public class InventoryController : Controller
    {
        private readonly WeinerDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public InventoryController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, WeinerDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            var model = _db.Items.ToList();
            return View(model);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult New(Item item)
        {
            item.Amount = 0;
            _db.Items.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Add(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult AddConfirmed(int id)
        {
            Item item = _db.Items.FirstOrDefault(i => i.ItemId == id);
            item.Amount += int.Parse(Request.Form["amount"]);
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Sale()
        {
            return View();
        }

        public IActionResult Refund()
        {
            return View();
        }
    }
}
