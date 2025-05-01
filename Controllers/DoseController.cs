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
        [Route("AddDose")]
        public async Task<ActionResult> AddDose([FromBody] DoseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Dose dose = new();

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
        public async Task<ActionResult> UpdateDose([FromBody] UpdateDoseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Dose? dose = await doseRepository.GetByIdAsync(dto.DoseId);

                if (dose == null)
                {
                    return NotFound("Dose record not found");
                }

                dose.SupplementId = dto.NewSupplementId;
                dose.DoseAmount = dto.NewDoseAmount;
                doseRepository.Update(dose);
                await doseRepository.SaveChangesAsync();

                return Ok(dose);
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
                if (await doseRepository.DeleteAsync(doseId))
                {
                    await doseRepository.SaveChangesAsync();
                    return Ok();
                }

                return NotFound("Dose record not found");
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

                List<Dose> doses = await doseRepository.GetSupplementDosesAsync(supplementId, userId);

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
