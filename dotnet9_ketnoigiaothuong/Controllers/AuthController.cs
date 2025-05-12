using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using FluentValidation;

namespace dotnet9_ketnoigiaothuong.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IValidator<LoginViewModel> _loginValidator;
        private readonly IValidator<RegisterViewModel> _registerValidator;

        public AuthController(IValidator<LoginViewModel> loginValidator, IValidator<RegisterViewModel> registerValidator)
        {
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerViewModel);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await Provider.AuthService.RegisterAsync(registerViewModel);
            if (response == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(new {
                response,
                redirect_url = "/auth/login"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginViewModel);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var user = await Provider.AuthService.Me(loginViewModel.Email);
            var response = await Provider.AuthService.LoginAsync(loginViewModel);

            var token = response.Data;
            if (string.IsNullOrEmpty(token))
            {
                return Ok(response);
            }

            Response.Cookies.Append("jwt", token!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
            });
            
            if (user.Data!.Role == "Admin")
            {
                return Ok(new { 
                    response,
                    redirect_url = "/admin/dashboard"
                });
            }
            else { 
                return Ok(new { 
                    response,
                    redirect_url = "/"
                });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { Data = true, IsSuccess = true, Message = "Logout successfully" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            //var username = User.Identity.Name;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await Provider.AuthService.Me(email!);
            if (user.Data == null)
            {
                return Ok(user);
            }
            return Ok(user);
        }
    }
}
