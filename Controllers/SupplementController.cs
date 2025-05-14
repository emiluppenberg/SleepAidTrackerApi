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

        [HttpGet]
        [Route("GetSupplement")]
        public async Task<ActionResult<SupplementDTO>> GetSupplement(int supplementId)
        {
            try
            {
                string userId = User.FindFirstValue("uid");

                Supplement supplement = await supplementRepository.GetByIdAsync(supplementId);

                if (userId != supplement.UserId)
                {
                    return Unauthorized();
                }

                SupplementDTO dto = new();
                mapper.Map(supplement, dto);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpPost]
        [Route("PostSupplement")]
        public async Task<ActionResult<SupplementDTO>> PostSupplement([FromBody] SupplementDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Problem();
            }
            try
            {
                Supplement supplement = new();
                string userId = User.FindFirstValue("uid");
                supplement.UserId = userId;

                mapper.Map(dto, supplement);

                await supplementRepository.AddAsync(supplement);
                await supplementRepository.SaveChangesAsync();

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateSupplement")]
        public async Task<ActionResult<SupplementDTO>> UpdateSupplement([FromBody] SupplementDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Problem();
            }
            try
            {
                string userId = User.FindFirstValue("uid");
                Supplement? supplement = await supplementRepository.GetByIdAsync(dto.Id);

                if (supplement == null)
                {
                    return NotFound("Supplement not found");
                }

                if (supplement.UserId != userId)
                {
                    return Unauthorized();
                }

                mapper.Map(dto, supplement);
                supplementRepository.Update(supplement);
                await supplementRepository.SaveChangesAsync();

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSupplement/{supplementId}")]
        public async Task<ActionResult> DeleteSleep(int supplementId)
        {
            try
            {
                if (await supplementRepository.DeleteAsync(supplementId))
                {
                    await supplementRepository.SaveChangesAsync();
                    return Ok();
                }

                return NotFound("Supplement was not found");
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUserSupplements")]
        public async Task<ActionResult<List<SupplementDTO>>> GetAllUserSupplements()
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
