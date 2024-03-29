﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ComplyCube.Net;
using ComplyCube.Net.Resources.Clients;
using ComplyCube.Net.Resources.SDKTokens;
using CustomApi.Models.Identity;
using DataAccess.Models.ApplicationModels;
using DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace UalaReplicaProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return BadRequest(new LoginResult
            {
                Successful = false,
                Error = "Username and password are invalid.",
                Expiration = DateTime.Now.AddMinutes(-4),
                Token = null
            });
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
        };
        var jwt = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
        return Ok(new LoginResult
        {
            Successful = true,
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiration = jwt.ValidTo,
            Error = ""
        });
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return BadRequest(new { Status = "Error", Message = "User already exists!" });

        var user = new ApplicationUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username,
            Address = new Address()
            {
                City = model.City,
                Province = model.Province,
                Road = model.Road,
                ZipCode = model.ZipCode
            },
            Accounts = new List<Account>()
            {
                new Account()
                {
                    CVU = new Random().NextInt64(1000000000000000000, long.MaxValue),
                    Balance = 0,
                    InvestedBalance = 0,
                    AccountNumber = new Random().NextInt64(1000000000000000000, long.MaxValue),
                    Alias = model.Username,
                    Url_ProfilePicture = $"$https://api.dicebear.com/6.x/initials/svg?size=64&seed={model.Username}"
                }
            }
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        return Ok(!result.Succeeded ? new { Status = "Error", Message = "User creation failed! Please check user details and try again." } : new { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("AddRole")]
    public async Task<IActionResult> AddRole(string role, string name)
    {
        var user = await _userManager.FindByNameAsync(name);

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(role))
        {
            await _userManager.AddToRoleAsync(user!, role);
        }

        return Ok(new { Status = "Success", Message = $"User \"{user}\" added to Role \"{role}\" successfully!" });
    }

    [HttpGet("GenerateToken/{id}")]
    public async Task<IActionResult> GenerateToken(string id)
    {
        var ccClient = new ComplyCubeClient("test_d2o3U25DeXNEZUVvbUs2cWg6NzkwYThkNzU5NWUzOTM1MGMwZTYwZDJiM2MxNTk3OWYwOTEzYmIzN2EwNWQ2ZjQ0ZjgxYWEyNTQ1MTQ3MDUwMQ==", new HttpClient(), new HttpClientHandler());
        var sdkTokenApi = new SDKTokenApi(ccClient);
        var sdkTokenRequest = new SDKTokenRequest { clientId = id, referrer = "*://*/*" };
        var sdkToken = await sdkTokenApi.GenerateToken(sdkTokenRequest);
        return Ok(sdkToken.token);
    }
}