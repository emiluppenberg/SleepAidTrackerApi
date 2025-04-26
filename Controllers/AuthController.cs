using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SleepAidTrackerApi.Data;
using SleepAidTrackerApi.Models.DTO.Auth;
using SleepAidTrackerApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SleepAidTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<SupplementController> logger;
        private readonly AppDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly TokenService tokenService;

        public AuthController(ILogger<SupplementController> logger, AppDbContext context, UserManager<IdentityUser> userManager, IConfiguration config, TokenService tokenService)
        {
            this.logger = logger;
            this.context = context;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            try
            {
                var user = await userManager.FindByEmailAsync(dto.Email);

                if (user == null)
                {
                    return Unauthorized(dto);
                }

                if (!await userManager.CheckPasswordAsync(user, dto.Password))
                {
                    return StatusCode(401);
                }

                string token = tokenService.GenerateToken(user);

                LoginResultDTO loginResult = new()
                {
                    Email = user.Email,
                    UserId = user.Id,
                    Token = token
                };

                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<LoginResultDTO>> Register([FromBody] RegisterRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            try
            {
                IdentityUser user = new()
                {
                    Email = dto.Email,
                    UserName = dto.Email
                };

                IdentityResult identityResult = await userManager.CreateAsync(user, dto.Password);

                if (identityResult.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(dto.Email);
                    string token = tokenService.GenerateToken(user);
                    LoginResultDTO loginResult = new()
                    {
                        Email = user.Email,
                        UserId = user.Id,
                        Token = token
                    };

                    return Ok(loginResult);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
