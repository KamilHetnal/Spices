using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using spices.Data;
using spices.Models;
using spices.Models.ViewModels;
using spices.Utility;

namespace spices.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
                Category = await _db.Category.ToListAsync(),
                Coupon = await _db.Coupon.Where(c => c.IsActive == true).ToListAsync()
            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                var cnt = _db.ShoppingCard.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssCardCount, cnt);
            }

            return View(IndexVM);
        }

        [Authorize]
        public async Task<IActionResult> Details(int Id)
        {
            var menuItemFromDb = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory)
                .Where(m => m.Id == Id).FirstOrDefaultAsync();

            ShoppingCard cardObj = new ShoppingCard()
            {
                MenuItem = menuItemFromDb,
                MenuItemId = menuItemFromDb.Id
            };

            return View(cardObj);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShoppingCard CardObject)
        {
            CardObject.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity) this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CardObject.ApplicationUserId = claim.Value;

                ShoppingCard cardFromDb = await _db.ShoppingCard.Where(c =>
                                                c.ApplicationUserId == CardObject.ApplicationUserId && c.MenuItemId == CardObject.MenuItemId)
                                                .FirstOrDefaultAsync();

                if (cardFromDb == null)
                {
                    await _db.ShoppingCard.AddAsync(CardObject);
                }
                else
                {
                    cardFromDb.Count = cardFromDb.Count + CardObject.Count;
                }

                await _db.SaveChangesAsync();

                var count = _db.ShoppingCard.Where(c => c.ApplicationUserId == CardObject.ApplicationUserId)
                    .ToList().Count;
                HttpContext.Session.SetInt32(SD.ssCardCount, count);

                return RedirectToAction("Index");
            }
            else
            {
                var menuItemFromDb = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory)
                                            .Where(m => m.Id == CardObject.MenuItemId).FirstOrDefaultAsync();

                ShoppingCard cardObj = new ShoppingCard()
                {
                    MenuItem = menuItemFromDb,
                    MenuItemId = menuItemFromDb.Id
                };

                return View(cardObj);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
