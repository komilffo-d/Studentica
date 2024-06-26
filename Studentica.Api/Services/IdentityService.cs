﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Requests.Project;
using Studentica.Common.Enums;
using Studentica.Database.Postgre.Models;
using Studentica.Identity.Common.Helpers;
using Studentica.Identity.Common;
using Studentica.Identity.Common.Models;
using Studentica.Infrastructure.Database.Repository.Project;
using Studentica.Services.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Studentica.Api.Services
{

    public interface IIdentityService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<(string, DateTime)?> Login(LoginModel model);
        Task<(IdentityUser?, bool, string message)> Register(RegisterModel model);
        Task<bool> Validate(string token);
    }

    public class IdentityService: IIdentityService<Guid>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public IdentityService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(string, DateTime)?> Login(LoginModel model)
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

                await _userManager.SetAuthenticationTokenAsync(user, "Login", "LoginToken", tokenJWT);

                return (tokenJWT, token.ValidTo);
            }
            return null;
        }

        public async Task<(IdentityUser?,bool, string message)> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username!);
            if (userExists != null)
                return (null, false, "Пользователь уже существует!");

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,

            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (!result.Succeeded)
                return (null,false, "Ошибка создания пользователя! Проверьре данные регистрации!");

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

            return (user, true, "Пользователь успешно зарегестрирован!");
        }

        public async Task<bool> Validate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = IdentityHelper.GetValidationParameters();
            TokenValidationResult validationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters);
            if (!validationResult.IsValid)
                return false;
            return true;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = IdentityHelper.SecurityKey;
            var token = new JwtSecurityToken(
                expires: DateTimeOffset.Now.AddDays(7).ToLocalTime().DateTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
