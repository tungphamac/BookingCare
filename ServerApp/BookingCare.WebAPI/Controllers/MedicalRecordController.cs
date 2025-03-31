using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MedicalRecordController> _logger;

        public MedicalRecordController(
            IMedicalRecordService medicalRecordService,
            IUnitOfWork unitOfWork,
            ILogger<MedicalRecordController> logger)
        {
            _medicalRecordService = medicalRecordService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecordCreateDto dto)
        {
            try
            {
                if (dto == null || !ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid medical record data provided.");
                    return BadRequest(new { Success = false, Message = "Invalid medical record data." });
                }

                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == userId)
                    .FirstOrDefaultAsync();
                if (doctor == null)
                {
                    _logger.LogWarning("Doctor not found for user ID {UserId}.", userId);
                    return Unauthorized(new { Success = false, Message = "Doctor not found." });
                }

                var record = new MedicalRecord
                {
                    AppointmentId = dto.AppointmentId,
                    Diagnosis = dto.Diagnosis,
                    Prescription = dto.Prescription,
                    Notes = dto.Notes,
                    CreatedBy = doctor.UserId
                };

                var result = await _medicalRecordService.AddMedicalRecordAsync(record, doctor.UserId);
                _logger.LogInformation($"Medical record {result} created successfully by Doctor ID {doctor.UserId}.");

                return Ok(new
                {
                    Success = true,
                    Message = "Thêm hồ sơ y tế thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new medical record.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] MedicalRecordUpdateDto dto)
        {
            try
            {
                if (dto == null || !ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid medical record data provided for update.");
                    return BadRequest(new { Success = false, Message = "Invalid medical record data." });
                }

                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == userId)
                    .FirstOrDefaultAsync();
                if (doctor == null)
                {
                    _logger.LogWarning("Doctor not found for user ID {UserId}.", userId);
                    return Unauthorized(new { Success = false, Message = "Doctor not found." });
                }

                var record = new MedicalRecord
                {
                    Id = id,
                    Diagnosis = dto.Diagnosis,
                    Prescription = dto.Prescription,
                    Notes = dto.Notes
                };

                var result = await _medicalRecordService.UpdateMedicalRecordAsync(record, doctor.UserId);
                if (!result)
                {
                    _logger.LogWarning($"Medical record with ID {id} not found.");
                    return NotFound(new { Success = false, Message = $"Medical record with ID {id} not found." });
                }

                _logger.LogInformation($"Medical record {id} updated successfully by Doctor ID {doctor.UserId}.");
                return Ok(new { Success = true, Message = "Cập nhật hồ sơ y tế thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating medical record with ID {id}.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> ViewMedicalRecord(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var record = await _medicalRecordService.ViewMedicalRecordAsync(id, userId);
                if (record == null)
                {
                    _logger.LogWarning($"Medical record with ID {id} not found.");
                    return NotFound(new { Success = false, Message = $"Medical record with ID {id} not found." });
                }

                var recordDto = new MedicalRecordDTO
                {
                    Id = record.Id,
                    AppointmentId = record.AppointmentId,
                    Diagnosis = record.Diagnosis,
                    Prescription = record.Prescription,
                    Notes = record.Notes,
                    CreatedAt = record.CreatedAt,
                    UpdatedAt = record.UpdatedAt,
                    CreatedBy = record.CreatedBy
                };

                return Ok(new { Success = true, Message = "Lấy hồ sơ y tế thành công", Data = recordDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving medical record with ID {id}.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }
    }
}
