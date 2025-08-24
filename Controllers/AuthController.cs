using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace KitchenEstimatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _userService.GetByEmailAsync(request.Email) != null)
                return BadRequest("Email already registered.");

            if (await _userService.GetByUsernameAsync(request.Username) != null)
                return BadRequest("Username already taken.");

            if (await _userService.GetByRoleAsync(request.Role) != null)
                return BadRequest("Select a role");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _userService.HashPassword(request.Password),
                Role = request.Role
            };

            await _userService.CreateAsync(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Email);
            if (user == null) return Unauthorized("Invalid email or password.");

            if (!_userService.VerifyPassword(user.PasswordHash, request.Password))
                return Unauthorized("Invalid username or password.");

            // 👉 Later: generate JWT token. For now, just return success
            return Ok(new { message = "Login successful", user = user.Username });
        }
    }

    // DTOs
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = null!;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
