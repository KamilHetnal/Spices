using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spices.Data;
using spices.Models;
using spices.Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace spices.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]

    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _db.Coupon.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupons)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    coupons.Pictures = p1;
                }
                _db.Coupon.Add(coupons);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coupons);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }

            var category = await _db.Coupon.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            if (coupon.Id == 0)
            {
                return NotFound();
            }

            var couponFromDb = await _db.Coupon.Where(c => c.Id == coupon.Id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    couponFromDb.Pictures = p1;
                }

                couponFromDb.MinimumAmount = coupon.MinimumAmount;
                couponFromDb.Name = coupon.Name;
                couponFromDb.Discount = coupon.Discount;
                couponFromDb.CouponType = coupon.CouponType;
                couponFromDb.IsActive = coupon.IsActive;

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(coupon);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }

            var category = await _db.Coupon.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var coupon = await _db.Coupon.FindAsync(id);
            if (coupon == null)
            {
                return View();
            }
            _db.Coupon.Remove(coupon);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }

            var coupon = await _db.Coupon.FindAsync(Id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }
    }
}


