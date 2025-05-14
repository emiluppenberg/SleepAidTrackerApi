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
                dto.Id = dose.Id;
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.ToString() ?? ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateDoses")]
        public async Task<ActionResult<List<DoseDTO>>> UpdateDoses([FromBody] List<DoseDTO> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                string userId = User.FindFirstValue("uid");
                List<Dose> doses = await doseRepository.GetDosesAsync(dtos);

                if (doses == null)
                {
                    return NotFound("Dose record not found");
                }

                foreach (var d in doses)
                {
                    if (d.UserId != userId)
                    {
                        return Unauthorized();
                    }
                }

                mapper.Map(dtos, doses);

                foreach (var d in doses)
                {
                    doseRepository.Update(d);
                }

                await doseRepository.SaveChangesAsync();

                return Ok(dtos);
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

        [HttpGet]
        [Route("GetAllUserDoses")]
        public async Task<ActionResult<List<DoseDTO>>> GetAllUserDoses()
        {
            try
            {
                string userId = User.FindFirstValue("uid")!;
                List<Dose> doses = await doseRepository.GetAllUserDosesAsync(userId);

                if (doses.Count < 1)
                {
                    return NotFound("No dose records found");
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
