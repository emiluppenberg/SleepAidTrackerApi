using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SleepAidTrackerApi.Data;
using SleepAidTrackerApi.Data.Repository;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO;

namespace SleepAidTrackerApi.Controllers
{
    [Route("[controller]")]
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
        public async Task<ActionResult> AddDose([FromBody] AddDoseDTO dto)
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
                return Problem(ex.InnerException.ToString());
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
                return Problem(ex.InnerException.ToString());
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
                if (ex.InnerException != null)
                {
                    return Problem(ex.InnerException.ToString());
                }

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSupplementDoses/{supplementId}")]
        public async Task<ActionResult> GetSupplementDoses(int supplementId)
        {
            try
            {
                List<Dose> doses = await doseRepository.GetSupplementDosesAsync(supplementId);

                if (doses.Count < 1)
                {
                    return NotFound("No dose records found");
                }

                return Ok(doses);
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
