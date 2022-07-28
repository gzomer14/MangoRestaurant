using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.ADMIN).Result is null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ADMIN)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.CUSTOMER)).GetAwaiter().GetResult();
            }
            else
                return;

            var adminUser = new ApplicationUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "48991501509",
                FirstName = "Admin",
                LastName = "Zomer"
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.ADMIN).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,adminUser.FirstName +" "+ adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.ADMIN),

            }).Result;

            var customerUser = new ApplicationUser()
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "48991501509",
                FirstName = "Customer",
                LastName = "Zomer"
            };

            _userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.CUSTOMER).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,customerUser.FirstName +" "+ customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.CUSTOMER),

            }).Result;
        }
    }
}
