using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Studentica.Identity.Common;
using Studentica.Identity.Common.Models;
using Studentica.Identity.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
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
                    new Claim("UserId", user.Id)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var tokenJWT = new JwtSecurityTokenHandler().WriteToken(token);

                await _userManager.SetAuthenticationTokenAsync(user,"Login","LoginToken", tokenJWT);

                return Ok(new ResponseModel
                {
                    Status = "Успешно",
                    Message="Предоставлен новый авторизационный токен",
                    Token = tokenJWT,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username!);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Ошибка", Message = "Пользователь уже существует!" });

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,

            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Ошибка", Message = "Ошибка создания пользователя! Проверьре данные регистрации!" });

            switch (model.Role)
            {
                case UserRoles.Student:
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Student.ToString()))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Student.ToString()));

                    if (await _roleManager.RoleExistsAsync(UserRoles.Student.ToString()))
                        await _userManager.AddToRoleAsync(user, UserRoles.Student.ToString());
                    break;
                case UserRoles.Teacher:
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Teacher.ToString()))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher.ToString()));

                    if (await _roleManager.RoleExistsAsync(UserRoles.Teacher.ToString()))
                        await _userManager.AddToRoleAsync(user, UserRoles.Teacher.ToString());
                    break;
                case UserRoles.Admin:
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
                    if (await _roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
                    break;
                default:
                    break;
            }

            return Ok(new ResponseModel { Status = "Успешно!", Message = "Пользователь успешно зарегестрирован!" });
        }

        [HttpGet]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromQuery] string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = TokenHelper.GetValidationParameters(_configuration);
            TokenValidationResult validationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters);
            if(!validationResult.IsValid)
                return Forbid();
            return Ok(token);


        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
