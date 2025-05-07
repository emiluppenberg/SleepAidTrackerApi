using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SleepAidTrackerApi.Data;
using SleepAidTrackerApi.Data.Repository;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO;
using SleepAidTrackerApi.Models.DTO.Action;
using SleepAidTrackerApi.Models.DTO.Base;
using System.Security.Claims;

namespace SleepAidTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoseController : ControllerBase
    {
        private readonly ILogger<DoseController> logger;
        private readonly IMapper mapper;
        private readonly DoseRepository doseRepository;
        private readonly UserManager<IdentityUser> userManager;

        public DoseController(ILogger<DoseController> logger, IMapper mapper, DoseRepository doseRepository, UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.doseRepository = doseRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("PostDose")]
        public async Task<ActionResult<DoseDTO>> PostDose([FromBody] DoseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Dose dose = new();

                string userId = User.FindFirstValue("uid");
                dose.UserId = userId;

                mapper.Map(dto, dose);

                await doseRepository.AddAsync(dose);
                await doseRepository.SaveChangesAsync();

                return Ok(dose);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateDose")]
        public async Task<ActionResult<DoseDTO>> UpdateDose([FromBody] DoseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                string userId = User.FindFirstValue("uid");
                Dose? dose = await doseRepository.GetByIdAsync(dto.Id);

                if (dose == null)
                {
                    return NotFound("Dose record not found");
                }
                if (dose.UserId != userId)
                {
                    return Unauthorized();
                }

                mapper.Map(dto, dose);
                doseRepository.Update(dose);
                await doseRepository.SaveChangesAsync();

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDose/{doseId}")]
        public async Task<ActionResult> DeleteDose(int doseId)
        {
            try
            {
                string userId = User.FindFirstValue("uid");
                Dose? dose = await doseRepository.GetByIdAsync(doseId);

                if (dose.UserId != userId)
                {
                    return Unauthorized();
                }
                if (!await doseRepository.DeleteAsync(doseId))
                {
                    return NotFound("Dose record not found");
                }

                await doseRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSupplementDoses/{supplementId}")]
        public async Task<ActionResult<List<DoseDTO>>> GetSupplementDoses(int supplementId)
        {
            try
            {
                string userId = User.FindFirstValue("uid")!;
                List<Dose> doses = await doseRepository.GetSupplementDosesAsync(supplementId);

                if (doses.Count < 1)
                {
                    return NotFound("No dose records found");
                }
                if (!doses.Any(x => x.UserId == userId))
                {
                    return Unauthorized();
                }

                List<DoseDTO> dtos = new();
                mapper.Map(doses, dtos);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }
    }
}
