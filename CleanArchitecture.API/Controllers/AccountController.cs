using CleanArchitecture.API.Utilities.Filters;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtService jwtService, IValidator<LoginDto> loginValidator, IValidator<RegisterDto> registerValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            //if(!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var validationResult = await _registerValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var user = model.Adapt<IdentityUser>();
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;

            var result = await _userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginDto model)
        {
            var validationResult = await _loginValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Not Found");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var token = await _jwtService.Generate(model.UserName);

                return Ok(new { Token = token });
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.SignOutAsync();            
            return Ok();
        }
    }
}
