using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PatientController> _logger;

        public PatientController(
            IPatientService patientService,
            UserManager<User> userManager,
            ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: api/patient/{id}
        [HttpGet("getby/{id}")]
        //[Authorize(Roles = "Admin,Doctor,Patient")] // Admin, Doctor, Patient có thể xem chi tiết bệnh nhân
        public async Task<ActionResult<PatientDetailDto>> GetPatientDetail(int id)
        {
            try
            {
                // Lấy UserId từ token
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Nếu là Patient, chỉ được xem thông tin của chính mình
                if (userRole == "Patient" && userId != id)
                {
                    _logger.LogWarning($"Patient with UserId {userId} attempted to access details of Patient with UserId {id}.");
                    return Unauthorized(new { Message = "Patients can only view their own details." });
                }

                var patient = await _patientService.GetPatientDetailAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning($"Patient with ID {id} not found.");
                    return NotFound(new { Message = $"Patient with ID {id} not found." });
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving patient with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the patient.");
            }
        }

        // GET: api/patient
        [HttpGet("getall")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được lấy danh sách bệnh nhân
        public async Task<ActionResult<IEnumerable<PatientDetailDto>>> GetPatients()
        {
            try
            {
                var patients = await _patientService.GetAllAsync();
                if (patients == null || !patients.Any())
                {
                    _logger.LogWarning("No patients found.");
                    return NotFound(new { Message = "No patients found." });
                }
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all patients.");
                return StatusCode(500, "An error occurred while retrieving patients.");
            }
        }

        // POST: api/patient/add
        [HttpPost("add")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được thêm bệnh nhân
        public async Task<IActionResult> AddPatient([FromBody] RegisterVm registerVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for AddPatient request.");
                    return BadRequest(ModelState);
                }

                var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
                if (userExists != null)
                {
                    _logger.LogWarning($"User {registerVm.Email} already exists.");
                    return BadRequest($"User {registerVm.Email} already exists");
                }

                // Tạo tài khoản người dùng mới
                var newUser = new User
                {
                    UserName = registerVm.UserName,
                    Email = registerVm.Email,
                    Gender = registerVm.Gender,
                    Address = registerVm.Address,
                    Avatar = registerVm.Avatar,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(newUser, registerVm.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequest(result.Errors);
                }

                // Gán vai trò "Patient" cho người dùng mới
                await _userManager.AddToRoleAsync(newUser, "Patient");

                // Tạo đối tượng Patient mới và gán UserId
                var newPatient = new Patient
                {
                    UserId = newUser.Id,
                    MedicalRecordId = registerVm.MedicalHistory
                };

                // Thêm bệnh nhân vào cơ sở dữ liệu
                await _patientService.AddPatientAsync(newPatient);

                _logger.LogInformation($"Patient {registerVm.Email} added successfully.");
                return Ok($"Patient {registerVm.Email} added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding patient with email {registerVm.Email}.");
                return StatusCode(500, "An error occurred while adding the patient.");
            }
        }

        // PUT: api/patient/update/{id}
        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Patient,Admin")] // Chỉ Admin/Patient được cập nhật bệnh nhân
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] RegisterVm updateVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for UpdatePatient request.");
                    return BadRequest(ModelState);
                }

                // Tìm người dùng cần cập nhật
                var existingUser = await _userManager.FindByIdAsync(id.ToString());
                if (existingUser == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound($"User with ID {id} not found.");
                }

                // Cập nhật thông tin người dùng
                existingUser.UserName = updateVm.UserName;
                existingUser.Email = updateVm.Email;
                existingUser.Gender = updateVm.Gender;
                existingUser.Address = updateVm.Address;
                existingUser.Avatar = updateVm.Avatar;

                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    _logger.LogError("Failed to update user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequest(result.Errors);
                }

                // Cập nhật thông tin bệnh nhân (Patient)
                var patient = await _patientService.GetByIdAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning($"Patient with ID {id} not found.");
                    return NotFound($"Patient with ID {id} not found.");
                }

                patient.MedicalRecordId = updateVm.MedicalHistory;

                // Sửa lại để gọi đúng phương thức UpdatePatientAsync
                var updateResult = await _patientService.UpdatePatientAsync(patient);
                if (!updateResult)
                {
                    _logger.LogError($"Error occurred while updating patient with ID {id}.");
                    return StatusCode(500, "An error occurred while updating the patient.");
                }

                _logger.LogInformation($"Patient with ID {id} updated successfully.");
                return Ok($"Patient with ID {id} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating patient with ID {id}.");
                return StatusCode(500, "An error occurred while updating the patient.");
            }
        }

        // DELETE: api/patient/delete/{id}
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được xóa bệnh nhân
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                // Tìm bệnh nhân trong cơ sở dữ liệu
                var patient = await _patientService.GetByIdAsync(id);
                if (patient == null)
                {
                    _logger.LogWarning($"Patient with ID {id} not found.");
                    return NotFound($"Patient with ID {id} not found.");
                }

                // Xóa bệnh nhân
                var result = await _patientService.DeleteAsync(patient);
                if (!result)
                {
                    _logger.LogError($"Error occurred while deleting patient with ID {id}.");
                    return StatusCode(500, "An error occurred while deleting the patient.");
                }

                // Xóa tài khoản người dùng liên kết với bệnh nhân
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user != null)
                {
                    var deleteResult = await _userManager.DeleteAsync(user);
                    if (!deleteResult.Succeeded)
                    {
                        _logger.LogError("Failed to delete user: {Errors}", string.Join(", ", deleteResult.Errors.Select(e => e.Description)));
                        return BadRequest(deleteResult.Errors);
                    }
                }

                _logger.LogInformation($"Patient with ID {id} and their account have been deleted successfully.");
                return Ok($"Patient with ID {id} and their account have been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting patient with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the patient.");
            }
        }

        // POST: api/patient/lock/{id}
        [HttpPost("lock/{id}")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được khóa/mở khóa tài khoản bệnh nhân
        public async Task<IActionResult> LockPatientAccount(int id, [FromQuery] DateTime lockUntil)
        {
            try
            {
                var result = await _patientService.LockUserAccountAsync(id, lockUntil);
                if (!result)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }

                _logger.LogInformation($"User account with ID {id} has been locked until {lockUntil}.");
                return Ok(new { Message = $"User account with ID {id} has been locked until {lockUntil}." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error locking account for user with ID {id}.");
                return StatusCode(500, "An error occurred while locking the account.");
            }
        }
    }
}