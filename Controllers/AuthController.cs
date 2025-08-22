using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    namespace ChatApp.Controllers
    {
        [Route("Account")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly SignInManager<IdentityUser> _signInManager;
            private readonly IConfiguration _configuration;

            public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _configuration = configuration;
            }

            [HttpPost("Register")]
            public async Task<IActionResult> Register([FromBody] RegisterModel model)
            {
                var user = new IdentityUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new { UserId = user.Id, Username = user.UserName });
                }
                return BadRequest(result.Errors);
            }

            [HttpPost("Login")]
            public async Task<IActionResult> Login([FromBody] LoginModel model)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty),
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? "your-secret-key-super-long-and-secure"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: "chat-app",
                        audience: "chat-app",
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creds);
                    return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), UserId = user.Id });
                }
                return Unauthorized();
            }
        }

        public class RegisterModel
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }

        public class LoginModel
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }
    }