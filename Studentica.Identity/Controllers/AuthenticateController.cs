using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Studentica.Identity.Data;
using Studentica.Identity.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Studentica.Identity.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username!);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register-student")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username!);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Ошибка", Message = "Пользователь уже существует!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Ошибка", Message = "Ошибка создания пользователя! Проверьре данные регистрации!" });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Student));

            if (await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _userManager.AddToRoleAsync(user, UserRoles.Student);

            return Ok(new Response { Status = "Успешно!", Message = "Пользователь успешно зарегестрирован!" });
        }

        [HttpPost]
        [Route("register-teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username!);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Ошибка", Message = "Пользователь уже существует!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Ошибка", Message = "Ошибка создания пользователя! Проверьре данные регистрации!" });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));

            if (await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _userManager.AddToRoleAsync(user, UserRoles.Teacher);
            
            return Ok(new Response { Status = "Успешно!", Message = "Пользователь успешно зарегестрирован!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username!);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Ошибка", Message = "Пользователь уже существует!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Ошибка", Message = "Ошибка создания пользователя! Проверьре данные регистрации!" });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Student));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Student))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Student);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Teacher))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Teacher);
            }
            return Ok(new Response { Status = "Успешно!", Message = "Пользователь успешно зарегестрирован!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
