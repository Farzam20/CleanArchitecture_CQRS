using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace CleanArchitecture.API.Utilities;

public class DataInitializer
{
    internal static void Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        context.Database.Migrate();
        InitData(context, userManager, roleManager);
    }

    private static void InitData(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        roleManager.CreateAsync(new IdentityRole("Guest")).Wait();

        var user = new IdentityUser()
        {
            UserName = "admin",
            Email = "f_yamini72@yahoo.com",
            PhoneNumber = "09215488280",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };

        userManager.CreateAsync(user, "qw12QW!@").Wait();
        userManager.AddToRoleAsync(user, "Admin").Wait();
    }
}

