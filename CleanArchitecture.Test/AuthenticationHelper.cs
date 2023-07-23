using CleanArchitecture.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.Test
{
    public static class AuthenticationHelper
    {
        public static async Task<string> GenerateToken(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtService jwtService, string username = "admin", string password = "qw12QW!@")
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return "User_Not_Found";
            }

            var token = await jwtService.Generate(username);

            return token;
        }
    }
}
