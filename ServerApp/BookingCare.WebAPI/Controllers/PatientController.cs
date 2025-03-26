using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/patient/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDetailDto>> GetPatientDetail(int id)
        {
            var patient = await _patientService.GetPatientDetailAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = $"Patient with ID {id} not found." });
            }
            return Ok(patient);
        }
        [HttpPut("Update-Partient{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePartientVm patient)
        {
            var updatedPatient = await _patientService.UpdatePatientAsync(id, patient);
            if (updatedPatient == null)
                return NotFound(new { message = "Không tìm thấy bệnh nhân hoặc cập nhật thất bại." });

            return Ok(updatedPatient);
        }
    }
}