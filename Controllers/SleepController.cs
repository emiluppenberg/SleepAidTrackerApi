using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SleepAidTrackerApi.Data;
using SleepAidTrackerApi.Data.Repository;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO.Action;
using SleepAidTrackerApi.Models.DTO.Base;
using System.Security.Claims;

namespace SleepAidTrackerApi.Controllers
{
    [Route("api/[controller]")]
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
        [Route("PostSleep")]
        public async Task<ActionResult<SleepDosesDTO>> PostSleep([FromBody] SleepDosesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                string userId = User.FindFirstValue("uid")!;

                Sleep sleep = new();
                mapper.Map(dto.Sleep, sleep);
                mapper.Map(dto.Doses, sleep.Doses);
                sleep.UserId = userId;
                foreach (var d in sleep.Doses)
                {
                    d.UserId = userId;
                }

                await sleepRepository.AddAsync(sleep);
                await sleepRepository.SaveChangesAsync();
                mapper.Map(sleep, dto.Sleep);
                mapper.Map(sleep.Doses, dto.Doses);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSleep")]
        public async Task<ActionResult<SleepDTO>> GetSleep(int sleepId)
        {
            try
            {
                string userId = User.FindFirstValue("uid")!;
                Sleep? sleep = await sleepRepository.GetByIdAsync(sleepId);

                if (sleep == null)
                {
                    return NotFound("Sleep record not found");
                }

                if (sleep.UserId != userId)
                {
                    return Unauthorized();
                }

                SleepDTO dto = new();
                mapper.Map(sleep, dto);

                return Ok(dto);
            }
            catch(Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateSleep")]
        public async Task<ActionResult<SleepDosesDTO>> UpdateSleep([FromBody] SleepDosesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                string userId = User.FindFirstValue("uid")!;
                Sleep? sleep = await sleepRepository.GetByIdAsync(dto.Sleep.Id);

                if (sleep == null)
                {
                    return NotFound("Sleep record not found");
                }
                if (sleep.UserId != userId)
                {
                    return Unauthorized();
                }

                mapper.Map(dto.Sleep, sleep);
                mapper.Map(dto.Doses, sleep.Doses);
                foreach (var d in sleep.Doses)
                {
                    d.UserId = userId;
                }
                sleepRepository.Update(sleep);
                await sleepRepository.SaveChangesAsync();
                mapper.Map(sleep, dto.Sleep);
                mapper.Map(sleep.Doses, dto.Doses);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
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
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUserSleeps")]
        public async Task<ActionResult<List<SleepDTO>>> GetAllUserSleeps()
        {
            try
            {
                string userId = User.FindFirstValue("uid");

                List<Sleep> sleeps = await sleepRepository.GetAllUserSleeps(userId);

                if (sleeps.Count < 1)
                {
                    return NotFound("No sleep records found");
                }

                List<SleepDTO> dtos = new();
                mapper.Map(sleeps, dtos);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserSleep/{sleepId}")]
        public async Task<ActionResult<SleepDTO>> GetUserSleep(int sleepId)
        {
            try
            {
                string userId = User.FindFirstValue("uid");
                
                Sleep? sleep = await sleepRepository.GetByIdAsync(sleepId);

                if (sleep == null)
                {
                    return NotFound("Sleep record not found");
                }

                if (sleep.UserId != userId)
                {
                    return Unauthorized();
                }

                SleepDTO dto = new();
                mapper.Map(sleep, dto);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }
    }
}
