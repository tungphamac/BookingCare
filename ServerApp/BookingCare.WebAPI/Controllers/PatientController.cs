using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        private readonly UserManager<User> _userManager;

        public PatientController(IPatientService patientService, UserManager<User> userManager)
        {
            _patientService = patientService;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDetailDto>>> GetPatients()
        {
            var patients = await _patientService.GetAllAsync();
            if (patients == null || !patients.Any())
            {
                return NotFound(new { Message = "No patients found." });
            }
            return Ok(patients);
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPatient([FromBody] RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
            if (userExists != null)
                return BadRequest($"User {registerVm.Email} already exists");

            // Tạo tài khoản người dùng mới
            var newUser = new User
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                Gender = registerVm.Gender,
                Address = registerVm.Address,
                Avatar = registerVm.Avatar,
                SecurityStamp = Guid.NewGuid().ToString()  // Đảm bảo có SecurityStamp khi tạo User
            };

            var result = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Gán vai trò "Patient" cho người dùng mới
            await _userManager.AddToRoleAsync(newUser, "Patient");

            // Tạo đối tượng Patient mới và gán UserId
            var newPatient = new Patient
            {
                UserId = newUser.Id,  // Gán UserId của tài khoản vừa tạo
                MedicalRecordId = registerVm.MedicalHistory // ID hồ sơ y tế (được truyền từ ViewModel)
            };

            // Thêm bệnh nhân vào cơ sở dữ liệu
            await _patientService.AddPatientAsync(newPatient);

            return Ok($"Patient {registerVm.Email} added successfully.");
        }

        // PUT: api/patient/update/{id}
        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] RegisterVm updateVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Tìm người dùng cần cập nhật
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null)
                return NotFound($"User with ID {id} not found.");

            // Cập nhật thông tin người dùng
            existingUser.UserName = updateVm.UserName;
            existingUser.Email = updateVm.Email;
            existingUser.Gender = updateVm.Gender;
            existingUser.Address = updateVm.Address;
            existingUser.Avatar = updateVm.Avatar;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Cập nhật thông tin bệnh nhân (Patient)
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound($"Patient with ID {id} not found.");

            // Cập nhật các thông tin khác của bệnh nhân
            patient.MedicalRecordId = updateVm.MedicalHistory;

            // Lưu lại thông tin bệnh nhân sau khi cập nhật
            var updateResult = await _patientService.UpdateAsync(patient);
            if (!updateResult)
                return StatusCode(500, "An error occurred while updating the patient.");

            return Ok($"Patient with ID {id} updated successfully.");
        }
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            // Tìm bệnh nhân trong cơ sở dữ liệu
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound($"Patient with ID {id} not found.");

            // Xóa bệnh nhân
            var result = await _patientService.DeleteAsync(patient);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the patient.");

            // Xóa tài khoản người dùng liên kết với bệnh nhân
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var deleteResult = await _userManager.DeleteAsync(user);
                if (!deleteResult.Succeeded)
                    return BadRequest(deleteResult.Errors);
            }

            return Ok($"Patient with ID {id} and their account have been deleted successfully.");
        }

    }
}