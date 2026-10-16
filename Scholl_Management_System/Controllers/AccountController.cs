using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Demo username/password
            if (model.Username == "admin" &&
                model.Password == "admin123")
            {
                // JWT token generate
                string token = GenerateToken(
                    model.Username,
                    "Admin"
                );

                // JWT cookie me save
                Response.Cookies.Append(
                    "jwt",
                    token,
                    new CookieOptions
                    {
                        HttpOnly = true,

                        // Local development ke liye false
                        Secure = false,

                        SameSite = SameSiteMode.Lax,

                        // 2 hours
                        Expires = DateTimeOffset.Now.AddHours(2)
                    }
                );

                return RedirectToAction(
                    "Index",
                    "Home"
                );
            }

            ViewBag.Error = "Invalid Username or Password";

            return View(model);
        }

        // Generate JWT
        private string GenerateToken(
            string username,
            string role)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    username
                ),

                new Claim(
                    ClaimTypes.Role,
                    role
                )
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],

                audience: _configuration["Jwt:Audience"],

                claims: claims,

                expires: DateTime.UtcNow.AddHours(2),

                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        // Logout
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return RedirectToAction("Login");
        }
    }
}