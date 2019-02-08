using AuctionsWeb2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuctionsWeb2.App_Start
{
    public class RoleConfig
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public RoleConfig()
        {
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public RoleConfig(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                ApplicationUser administrator = new ApplicationUser()
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
                // pravi admin user-a
                await _userManager.CreateAsync(administrator, "password");
                // pravi admin ulogu
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                // povezuje korisnika sa ulogom
                IdentityResult result = await _userManager.AddToRoleAsync(administrator.Id, "admin");
            }
        }
    }
}