using AspNet.Security.OpenId.Steam;
using DMRVAPI.Repositories.DataModel;
using DMRVAPI.Repositories.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DMRVAPI.Controllers
{
    [Route("/user")]
    [EnableCors("WebAPI")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IMariaDbUserService _userDb;
        private readonly IConfiguration _configuration;

        public UserDataController(ILogger<UserDataController> logger, IMariaDbUserService mariaDbUserService, IConfiguration configuration)
        {
            _userDb = mariaDbUserService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SteamAuthenticationDefaults.AuthenticationScheme)]
        [Route("get-token")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                string name = User.Claims.FirstOrDefault<Claim>(claim => claim.Type.Equals(ClaimTypes.Name))?.Value ?? throw new NullReferenceException();
                string nameIdentifier = User.Claims.FirstOrDefault<Claim>(claim => claim.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? throw new NullReferenceException();
                
                // nameIdentifier: https://steamcommunity.com/openid/id/76561198215495593
                string steam64String = nameIdentifier.Split('/').Last();
                ulong steam64Id = UInt64.Parse(steam64String);

                if(await _userDb.GetViaSteamId(steam64Id) == null)
                {
                    // Registration
                    UserDataModel newUser = new UserDataModel()
                    {
                        steam_id = steam64Id,
                        private_flag = false,
                        last_update = DateTime.Now,
                        steam_last_update = DateTime.Now,
                        role = "User"
                    };

                    await _userDb.Insert(newUser);
                    await _userDb.UpdateSteamInfo(steam64Id);

                    // Get JwtToken
                    var user = await _userDb.GetViaSteamId(steam64Id);

                    if (user != null) {
                        string jwt = GenerateJwtToken(user);
                        return Ok(new
                        {
                            jwt = jwt,
                        });
                    }
                    else
                    {
                        return Ok(user);
                    }
                }
                else
                {
                    // Get JwtToken
                    var user = await _userDb.GetViaSteamId(steam64Id);

                    if (user != null)
                    {
                        string jwt = GenerateJwtToken(user);
                        return Ok(new
                        {
                            jwt = jwt,
                        });
                    }
                    else
                    {
                        return Ok(user);
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // Update User Account Info, Close Account ...  etc

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        [Route("update-steam")]
        public async Task<IActionResult> UpdateSteamData()
        {
            try
            {
                ClaimsPrincipal currentUser = HttpContext.User!;
                string steam64Str = currentUser.Claims.FirstOrDefault(c => c.Type == "steam")?.Value ?? throw new ArgumentNullException();
                ulong steam64Id = UInt64.Parse(steam64Str);

                var user = await _userDb.GetViaSteamId(steam64Id);

                if (DateTime.Compare(user!.steam_last_update!.Value.AddMinutes(5), DateTime.Now) > 0)
                {
                    return StatusCode(StatusCodes.Status429TooManyRequests);
                }
                await _userDb.UpdateSteamInfo(steam64Id);

                return Ok(await _userDb.GetViaSteamId(steam64Id));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private string GenerateJwtToken(UserDataModel user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ConnectionStrings:Jwt:SecretKey"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()!),
                new Claim("steam", user.steam_id.ToString()!),
                new Claim(ClaimTypes.Role, user.role!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(issuer: _configuration["ConnectionStrings:Jwt:Issuer"],
                                             audience: _configuration["ConnectionStrings:Jwt:Issuer"],
                                             claims: claims,
                                             notBefore: DateTime.Now,
                                             expires: DateTime.Now.AddMonths(2),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
