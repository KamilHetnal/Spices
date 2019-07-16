using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using spices.Data;
using spices.Models;
using spices.Utility;

namespace spices.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }

            var category = await _db.Category.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }

            var category = await _db.Category.FindAsync(Id);
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
            
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return View();
            }
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();

            }

            var category = await _db.Category.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}