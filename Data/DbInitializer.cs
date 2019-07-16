using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using spices.Models;
using spices.Utility;

namespace spices.Data
{
    public class DbInitializer : IDbInitializer
    {
        public readonly ApplicationDbContext _db;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void  Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if(_db.Roles.Any(r=>r.Name == SD.ManagerUser)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.CustomerUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.FrontDeskUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.KitchenUser)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Kamil Hetnal",
                EmailConfirmed = true,
                PhoneNumber = "111222333"
            },"Admin123*").GetAwaiter().GetResult();

            IdentityUser user = await _db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");

            await _userManager.AddToRoleAsync(user, SD.ManagerUser);
        }
    }
}
