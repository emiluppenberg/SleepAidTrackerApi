using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SleepAidTrackerApi.Data;
using SleepAidTrackerApi.Data.Repository;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO;

namespace SleepAidTrackerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SleepController : ControllerBase
    {
        private readonly ILogger<SleepController> logger;
        private readonly IMapper mapper;
        private readonly SleepRepository sleepRepository;
        private readonly UserManager<IdentityUser> userManager;

        public SleepController(ILogger<SleepController> logger, IMapper mapper, SleepRepository sleepRepository, UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.sleepRepository = sleepRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("AddSleep")]
        public async Task<ActionResult> AddSleep([FromBody] AddSleepDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Sleep sleep = new();
                mapper.Map(dto, sleep);
                await sleepRepository.AddAsync(sleep);
                await sleepRepository.SaveChangesAsync();
                return Ok(sleep);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException.ToString());
            }
        }

        [HttpPost]
        [Route("UpdateSleepDate")]
        public async Task<ActionResult> UpdateSleepDate([FromBody] UpdateSleepDateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Sleep? sleep = await sleepRepository.GetByIdAsync(dto.SleepId);

                if (sleep == null)
                {
                    return NotFound("Sleep record was not found");
                }

                sleep.SleepDate = dto.NewDate;

                sleepRepository.Update(sleep);
                await sleepRepository.SaveChangesAsync();

                return Ok(sleep);
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

        [HttpPost]
        [Route("UpdateSleepHours")]
        public async Task<ActionResult> UpdateSleepHours([FromBody] UpdateSleepHoursDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Sleep? sleep = await sleepRepository.GetByIdAsync(dto.SleepId);

                if (sleep == null)
                {
                    return NotFound("Sleep record was not found");
                }

                sleep.HoursOfSleep = dto.NewHoursOfSleep;

                sleepRepository.Update(sleep);
                await sleepRepository.SaveChangesAsync();

                return Ok(sleep);
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

        [HttpDelete]
        [Route("DeleteSleep/{sleepId}")]
        public async Task<ActionResult> DeleteSleep(int sleepId)
        {
            try
            {
                if (await sleepRepository.DeleteAsync(sleepId))
                {
                    await sleepRepository.SaveChangesAsync();
                    return Ok();
                }

                return NotFound("Sleep record was not found");
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
        [Route("GetUserSleeps/{userId}")]
        public async Task<ActionResult> GetUserSleeps(string userId)
        {
            try
            {
                List<Sleep> sleeps = await sleepRepository.GetUserSleeps(userId);

                if (sleeps.Count < 1)
                {
                    return NotFound("No sleep records found");
                }

                return Ok(sleeps);
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
