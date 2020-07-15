using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LogApi.Models;
using LogApi.Utils;
using LogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LogApi.Controllers
{
    [Authorize]
    public class AuthController : MyControllerBase
    {
        private readonly IConfiguration _config;
        private readonly LoggingDbContext _context;

        public AuthController(LoggingDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Users.Where(x => x.UserName == registerDto.UserName).AnyAsync())
                return BadRequest(new Error("User already exits"));

            byte[] passwordHash, passwordSalt;
            PasswordUtil.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            var newUser = new User();
            newUser.UserName = registerDto.UserName;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var tokenString = GetToken(newUser);
            return Ok(new { token = tokenString });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null || !PasswordUtil.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return NotFound();

            var tokenString = GetToken(user);

            return Ok(new { token = tokenString });

        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}