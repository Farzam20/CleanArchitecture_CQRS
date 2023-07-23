using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Test
{
    internal class DatabaseFixture : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseFixture(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;

            _context.Database.EnsureCreated();

            InitData();
        }

        private void InitData()
        {
            _roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            _roleManager.CreateAsync(new IdentityRole("Guest")).Wait();

            var user = new IdentityUser()
            {
                UserName = "admin",
                Email = "f_yamini72@yahoo.com",
                PhoneNumber = "09215488280",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            _userManager.CreateAsync(user, "qw12QW!@").Wait();
            _userManager.AddToRoleAsync(user, "Admin").Wait();

            if(!_context.Set<Product>().Any())
            {
                _context.Set<Product>().Add(new Product() 
                { 
                    CreatedByUserId = user.Id, 
                    Name = "Test_01", 
                    ManufactureEmail = "farzamyamini@yahoo.com", 
                    ManufacturePhone = "09027159171", 
                    ProduceDate = DateTime.Now, 
                    IsAvailable = true 
                });

                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            //_context.Database.EnsureDeleted();
        }
    }
}
