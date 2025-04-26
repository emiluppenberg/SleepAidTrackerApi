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
        [Route("PostAddSleep")]
        public async Task<ActionResult> PostAddSleep([FromBody] SleepDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                string userId = User.FindFirstValue("uid")!;

                Sleep sleep = new();
                mapper.Map(dto, sleep);
                sleep.UserId = userId;
                foreach (var d in sleep.Doses)
                {
                    d.UserId = userId;
                }

                await sleepRepository.AddAsync(sleep);
                await sleepRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException.ToString());
            }
        }

        [HttpGet]
        [Route("GetEditSleep")]
        public async Task<ActionResult<SleepDTO>> GetEditSleep(int sleepId)
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
                if (ex.InnerException != null)
                {
                    return Problem(ex.InnerException.ToString());
                }

                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("PutEditSleep")]
        public async Task<ActionResult> PutEditSleep(SleepDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                string userId = User.FindFirstValue("uid")!;
                Sleep? sleep = await sleepRepository.GetByIdAsync(dto.Id!.Value);

                if (sleep == null)
                {
                    return NotFound("Sleep record not found");
                }

                if (sleep.UserId != userId)
                {
                    return Unauthorized();
                }

                mapper.Map(dto, sleep);
                sleepRepository.Update(sleep);
                await sleepRepository.SaveChangesAsync();

                return Ok();
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
