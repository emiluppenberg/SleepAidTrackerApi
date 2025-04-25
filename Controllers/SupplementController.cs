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

namespace SleepAidTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Route("AddSupplement")]
        public async Task<ActionResult> AddSupplement([FromBody] AddSupplementDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Problem();
            }
            try
            {
                Supplement supplement = new()
                {
                    Name = dto.Name,
                    Unit = dto.Unit,
                    UserId = dto.UserId
                };

                await supplementRepository.AddAsync(supplement);
                await supplementRepository.SaveChangesAsync();

                return Ok(supplement);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return Problem(ex.InnerException.ToString());
                }

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserSupplements")]
        public async Task<ActionResult> GetUserSupplements(string userId)
        {
            try
            {
                List<Supplement> supplements = await supplementRepository.GetUserSupplements(userId);

                return Ok(supplements);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return Problem(ex.InnerException.ToString());
                }

                return Problem(ex.Message);
            }
        }
    }
}
