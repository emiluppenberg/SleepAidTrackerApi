using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SleepAidTrackerApi.Data;
using SleepAidTrackerApi.Data.Repository;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO;
using SleepAidTrackerApi.Models.DTO.Base;
using System.Security.Claims;

namespace SleepAidTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SupplementController : Controller
    {
        private readonly ILogger<SupplementController> logger;
        private readonly SupplementRepository supplementRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public SupplementController(ILogger<SupplementController> logger, SupplementRepository supplementRepository, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.logger = logger;
            this.supplementRepository = supplementRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("PostAddSupplement")]
        public async Task<ActionResult> PostAddSupplement([FromBody] SupplementDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Problem();
            }
            try
            {
                string userId = User.FindFirstValue("uid");

                Supplement supplement = new()
                {
                    Name = dto.Name,
                    Unit = dto.Unit,
                    UserId = userId
                };

                await supplementRepository.AddAsync(supplement);
                await supplementRepository.SaveChangesAsync();

                return Ok(supplement);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserSupplements")]
        public async Task<ActionResult<List<SupplementDTO>>> GetUserSupplements()
        {
            try
            {
                string userId = User.FindFirstValue("uid")!;

                List<Supplement> supplements = await supplementRepository.GetUserSupplements(userId);
                List<SupplementDTO> dtos = new();

                mapper.Map(supplements, dtos);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }
    }
}
