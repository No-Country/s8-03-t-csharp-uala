using CustomApi.Models.Identity;
using DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ComplyCube.Net;
using ComplyCube.Net.Model;
using ComplyCube.Net.Resources.Clients;
using ComplyCube.Net.Resources.SDKTokens;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {       
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {                    
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            authClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            authClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new LoginResult
            {
                Successful = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Error = ""
            });
        }
        return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid.",Expiration = DateTime.Now.AddMinutes(-4),Token= null });
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        return Ok(new { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("AddRole")]
    public async Task<IActionResult> AddRole(string Role, string name)
    {
        var user = await userManager.FindByNameAsync(name);

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }

        return Ok(new { Status = "Success", Message = "User created successfully!" });
    }

    [HttpGet("GenerateToken/{id}")]
    public async Task<IActionResult> GenerateToken(string id)
    {
        var ccClient = new ComplyCubeClient("test_d2o3U25DeXNEZUVvbUs2cWg6NzkwYThkNzU5NWUzOTM1MGMwZTYwZDJiM2MxNTk3OWYwOTEzYmIzN2EwNWQ2ZjQ0ZjgxYWEyNTQ1MTQ3MDUwMQ==",new HttpClient(), new HttpClientHandler());

        var clientApi = new ClientApi(ccClient);

        var clients = await clientApi.ListAsync();

        var sdkTokenApi = new SDKTokenApi(ccClient);

        var sdkTokenRequest = new SDKTokenRequest { clientId = id, referrer = "*://*/*" };

        var sdkToken = await sdkTokenApi.GenerateToken(sdkTokenRequest);
        return Ok(sdkToken.token);
    }
}


