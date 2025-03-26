using BookingCare.Business.Services;
using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorCreateRequest>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();

            // Chỉ lấy một số trường cần thiết từ mỗi bệnh nhân
            var patientDTOs = patients.Select(p => new PatientDto
            {
                PatientId = p.UserId,
                FullName = p.User.UserName,
                Email = p.User.Email,
                PhoneNumber = p.User.PhoneNumber
            }).ToList();

            return Ok(patientDTOs); // Trả về danh sách các PatientDTO
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound(); // Nếu không tìm thấy bệnh nhân, trả về lỗi 404
            }

            return Ok(patient); // Trả về bệnh nhân tìm thấy
        }

        [HttpPost]
        public async Task<ActionResult<DoctorCreateRequest>> CreatePatient(DoctorCreateRequest patientDTO)
        {
            if (patientDTO == null)
            {
                return BadRequest("PatientDTO is required.");
            }

            // Kiểm tra xem MedicalRecordId có tồn tại trong bảng MedicalRecords không
           

            // Convert DTO to Patient entity
            var patient = new Patient
            {
                User = new User
                {
                    UserName = patientDTO.User.UserName,
                    Gender = patientDTO.User.Gender,
                    PhoneNumber = patientDTO.User.PhoneNumber,
                    Avatar = patientDTO.User.Avatar ?? "default-avatar.jpg"
                },
                
            };

            await _patientService.AddPatientAsync(patient);

            return CreatedAtAction(nameof(GetPatientById), new { id = patient.UserId }, patientDTO);
        }


        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, DoctorCreateRequest patientDTO)
        {
            if (id != patientDTO.User.UserId)
            {
                return BadRequest("Patient ID mismatch");
            }

            var existingPatient = await _patientService.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            // Convert DTO to Patient entity for update
            existingPatient.User.UserName = patientDTO.User.UserName;
            existingPatient.User.Gender = patientDTO.User.Gender;
            existingPatient.User.PhoneNumber = patientDTO.User.PhoneNumber;
            existingPatient.User.Email = patientDTO.User.Email;

            await _patientService.UpdatePatientAsync(existingPatient);

            return NoContent();
        }

        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound(); // Nếu không tìm thấy bệnh nhân, trả về lỗi 404
            }

            await _patientService.DeletePatientAsync(id);
            return NoContent(); // Trả về status 204 khi xóa thành công
        }
    }
}